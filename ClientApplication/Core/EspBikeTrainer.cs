using Avans.TI.BLE;
using ClientApplication.Data;
using ClientApplication.Exception;
using ClientApplication.Interface;
using RHApplicationLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace ClientApplication.Core
{
    public class EspBikeTrainer : IBikeTrainer
    {

        // BLE Service Identifiers
        private const string BIKE_SERVICE = "6e40fec1-b5a3-f393-e0a9-e50e24dcca9e";
        private const string BIKE_RECEIVE_CHARACTERISTIC = "6e40fec2-b5a3-f393-e0a9-e50e24dcca9e";
        private const string BIKE_SEND_CHARACTERISTIC = "6E40FEC3-B5A3-F393-E0A9-E50E24DCCA9E";
        private const string HEART_SERVICE = "HeartRate";
        private const string HEART_RATE_CHARACTERISTIC = "HeartRateMeasurement";

        // ANT protocol
        private const byte ANT_SYNC_BYTE = 0xA4;

        private const byte GENERAL_FE_DATA_PAGE = 0x10;
        private const byte SPECIFIC_BIKE_DATA_PAGE = 0x19;

        // Events
        public event BikeDataReceivedEventHandler BikeDataReceived;
        public event BikeConnectionStateChanged BikeConnectionChanged;

        // public
        private BikeConnectionState _connectionState; // DONT USE
        public BikeConnectionState ConnectionState
        {
            get
            {
                return this._connectionState;
            }
            private set
            {
                this._connectionState = value;
                if (this._connectionState != BikeConnectionState.Error)
                    this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(this._connectionState));
            }
        }

        // constructor params
        private readonly string bikeName;

        // private
        private BLE bleBike;
        private BLE bleHeart;

        private Timer resistanceSetTimer;
        private int resistance = 0;

        public EspBikeTrainer(string bikeName)
        {
            if (string.IsNullOrEmpty(bikeName)) throw new ArgumentNullException(nameof(bikeName));

            this.bikeName = bikeName;



            this.resistanceSetTimer = new Timer();
            this.resistanceSetTimer.Interval = 250; // send with 4hz, see bike specs
            this.resistanceSetTimer.AutoReset = true;
            this.resistanceSetTimer.Elapsed += ResistanceSetTimer_Tick;
        }

        public async void StartReceiving()
        {
            if (this.ConnectionState != BikeConnectionState.Connected)
            {
                this.bleBike = new BLE();
                this.bleHeart = new BLE();

                await Task.Delay(1000); // We need some time to list available devices

                int errorCode = 0;

                List<string> bleBikeList = this.bleBike.ListDevices();

                if (bleBikeList.Contains(this.bikeName))
                {
                    try
                    {
                        errorCode = await this.bleBike.OpenDevice(this.bikeName);
                        if (errorCode != 0)
                        {
                            this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                                BikeConnectionState.Error,
                                new BLEException(errorCode, $"Cannot connect to {this.bikeName}. Make sure it's turned on!")));

                            return;
                        }

                        errorCode = await this.bleBike.SetService(EspBikeTrainer.BIKE_SERVICE);
                        if (errorCode != 0)
                        {
                            this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                                BikeConnectionState.Error,
                                new BLEException(errorCode, $"Cannot connect to {this.bikeName} bike service.")));

                            return;
                        }

                        this.bleBike.SubscriptionValueChanged += BleBikeValueReceived;
                        errorCode = await this.bleBike.SubscribeToCharacteristic(EspBikeTrainer.BIKE_RECEIVE_CHARACTERISTIC);
                        if (errorCode != 0)
                        {
                            this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                                BikeConnectionState.Error,
                                new BLEException(errorCode, $"Cannot subscribe to {this.bikeName} receive characteristic.")));

                            return;
                        }

                        errorCode = await this.bleHeart.OpenDevice(this.bikeName);
                        if (errorCode != 0)
                        {
                            this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                                BikeConnectionState.Error,
                                new BLEException(errorCode, $"Cannot connect to {this.bikeName}. Make sure it's turned on!")));

                            return;
                        }

                        errorCode = await this.bleHeart.SetService(EspBikeTrainer.HEART_SERVICE);
                        if (errorCode != 0)
                        {
                            this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                                BikeConnectionState.Error,
                                new BLEException(errorCode, $"Cannot connect to {this.bikeName} heartrate service.")));

                            return;
                        }

                        this.bleHeart.SubscriptionValueChanged += BleHeartValueReceived;
                        errorCode = await this.bleHeart.SubscribeToCharacteristic(EspBikeTrainer.HEART_RATE_CHARACTERISTIC);
                        if (errorCode != 0)
                        {
                            this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                                BikeConnectionState.Error,
                                new BLEException(errorCode, $"Cannot subscribe to {this.bikeName} heartrate characteristic.")));

                            return;
                        }
                        else
                        {
                            this.ConnectionState = BikeConnectionState.Connected;

                            // Reset Resistance
                            this.SetResistance(0);
                            this.resistanceSetTimer.Start();
                        }
                    }
                    catch
                    {

                    }
                    finally
                    {
                        if (errorCode != 0)
                        {
                            this.resistanceSetTimer.Stop();
                            this.bleBike.CloseDevice();
                            this.bleHeart.CloseDevice();

                            this.bleBike.Dispose();
                            this.bleHeart.Dispose();

                            this.ConnectionState = BikeConnectionState.Disconnected;
                        }

                    }
                }
                else
                {
                    this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                        BikeConnectionState.Error,
                        new BLEException(errorCode, $"Cannot find {this.bikeName}. Make sure it's turned on!")));
                }
            }
        }

        public void StopReceiving()
        {
            if (this.ConnectionState == BikeConnectionState.Connected)
            {
                try
                {
                    this.resistanceSetTimer.Stop();

                    if(this.bleBike != null)
                    {
                        //this.bleBike.CloseDevice();
                        this.bleBike.Dispose();

                        this.bleBike = null;
                    }
                    
                    if(this.bleHeart != null)
                    {
                        //this.bleHeart.CloseDevice();
                        this.bleHeart.Dispose();

                        this.bleHeart = null;
                    }
                }
                catch
                {
                    // jammer dan
                }

                this.ConnectionState = BikeConnectionState.Disconnected;
            }
        }

        public void SetResistance(int resistance)
        {
            this.resistance = resistance;
        }

        private byte GenerateChecksum(IEnumerable<byte> packet) => packet.Aggregate((byte)0, (accu, elem) => accu ^= elem);

        #region Events

        private void BleHeartValueReceived(object sender, BLESubscriptionValueChangedEventArgs e)
        {
            if (e.Data.Length == 2) // if false: not a ushort, so data corrupted
            {
                this.BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.HeartBeat, new DBikeHeartBeat()
                {
                    HeartBeat = BitConverter.ToUInt16(e.Data.Reverse().ToArray(), 0)
                }));
            }
        }

        private void BleBikeValueReceived(object sender, BLESubscriptionValueChangedEventArgs e)
        {
            if (e.Data.Length > 3)
            {
                byte syncByte = e.Data[0];
                byte dataLength = e.Data[1];
                byte type = e.Data[2];
                byte channel = e.Data[3];
                byte checksum = e.Data[e.Data.Length - 1];

                byte calculatedChecksum = this.GenerateChecksum(e.Data.Take(e.Data.Length - 1));

                if (syncByte == EspBikeTrainer.ANT_SYNC_BYTE
                    && checksum == calculatedChecksum
                    && dataLength == 9                      // dataLength is always 9 for FE-C
                    && e.Data.Length == 3 + dataLength + 1) // syncByte + lengthByte + typeByte + dataLength (includes the channel byte) + checksumByte
                {
                    // skip sync-, length-, type- and channelbyte. remove 1 from datalength because we skipped channelByte
                    byte[] data = e.Data.Skip(4).Take(dataLength - 1).ToArray();

                    byte pageNumber = data[0];

                    if (pageNumber == EspBikeTrainer.GENERAL_FE_DATA_PAGE)
                    {
                        DBikeGeneralFEData result = new DBikeGeneralFEData()
                        {
                            EquipmentTypeField = data[1],
                            ElapsedTime = data[2],
                            DistanceTraveled = data[3],
                            Speed = BitConverter.ToUInt16(Utility.ReverseIfBigEndian(data.Skip(4).Take(2)).ToArray(), 0),
                            HeartRate = data[6],
                            CapabilitiesField = (byte)(data[7] & 0b1111), // Capabilities field is the first nibble of the last byte
                            FEStateField = (byte)((data[7] >> 4) & 0b1111) // FE State field is the last nibble of the last byte
                        };

                        this.BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.GeneralFEData, result));
                    }
                    if (pageNumber == EspBikeTrainer.SPECIFIC_BIKE_DATA_PAGE)
                    {
                        byte[] powerBytes = new byte[] { data[5], (byte)(data[6] & 0b1111) }; // Length of power is 1.5 bytes

                        DBikeSpecificBikeData result = new DBikeSpecificBikeData()
                        {
                            UpdateEventCount = data[1],
                            Cadence = data[2],
                            PowerGenerated = BitConverter.ToUInt16(Utility.ReverseIfBigEndian(data.Skip(3).Take(2)).ToArray(), 0),
                            Power = BitConverter.ToUInt16(Utility.ReverseIfBigEndian(powerBytes).ToArray(), 0),
                            TrainerStatusField = (byte)((data[6] >> 4) & 0b1111), // Trainer Status field is the last nibble of the 6th byte
                            FlagsField = (byte)(data[7] & 0b1111), // Flags field is the first nibble of the last byte
                            FEStateField = (byte)((data[7] >> 4) & 0b1111) // FE State field is the last nibble of the last byte
                        };

                        this.BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.SpecificBikeData, result));
                    }
                }
            }
        }

        private async void ResistanceSetTimer_Tick(object sender, EventArgs e)
        {
            List<byte> message = new List<byte>() { 0x4A, 0x09, 0x4E, 0x05, 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, (byte)this.resistance };

            message.Add(this.GenerateChecksum(message));

            try
            {
                await this.bleBike.WriteCharacteristic(EspBikeTrainer.BIKE_SEND_CHARACTERISTIC, message.ToArray());
            }
            catch
            {
                this.BikeConnectionChanged?.Invoke(this, new BikeConnectionStateChangedEventArgs(
                                BikeConnectionState.Error,
                                new BLEException(0, $"The connection with {this.bikeName} has been lost.")));
            }
        }

        #endregion
    }
}
