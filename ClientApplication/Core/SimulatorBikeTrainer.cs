using ClientApplication.Data;
using ClientApplication.Interface;
using System;
using System.Windows.Forms;

namespace ClientApplication.Core
{
    public class SimulatorBikeTrainer : IBikeTrainer
    {
        public event BikeDataReceivedEventHandler BikeDataReceived;
        public event BikeConnectionStateChanged BikeConnectionChanged;

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

        private ushort heartbeat;
        private int speed;
        private double time;
        private int distance;
        private Timer sendTimer;
        private int power;
        private int resistance;

        public SimulatorBikeTrainer(TrackBar speedTrackBar, TrackBar bpmTrackBar)
        {
            speedTrackBar.ValueChanged += SpeedTrackBar_Scroll;
            bpmTrackBar.ValueChanged += HeartBeatTrackBar_Scroll;

            sendTimer = new Timer();
            sendTimer.Interval = 250;
            sendTimer.Tick += this.sendTimer_Tick;

            heartbeat = (ushort)bpmTrackBar.Value;
            speed = speedTrackBar.Value * 10;
            time = 0.0;
            distance = 0;
            power = 0;
            resistance = 0;
        }

        private void sendTimer_Tick(object sender, EventArgs e)
        {
            time += 0.25;
            if (time > 64) time = 0;

            distance = (int)Math.Round(0.25 * (speed / 1000.0)) + distance;
            if (distance > 255) distance = 0;

            DBikeGeneralFEData generalData = new DBikeGeneralFEData
            {
                Speed = (ushort)speed,
                ElapsedTime = (byte)(time * 4),
                DistanceTraveled = (byte)distance,
            };
            BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.GeneralFEData, generalData));

            DBikeSpecificBikeData specificData = new DBikeSpecificBikeData
            {
                Power = (ushort)power
            };
            BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.SpecificBikeData, specificData));

            DBikeHeartBeat heartBeatData = new DBikeHeartBeat
            {
                HeartBeat = this.heartbeat
            };
            BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.HeartBeat, heartBeatData));
        }

        private void SpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            this.speed = ((TrackBar)sender).Value * 10;
            this.power = (int)(Math.Min(250, (speed / 1000.0) * 12.21) + Math.Min(250, ((speed / 1000.0) * 12.21) * resistance / 200.0));
        }

        private void HeartBeatTrackBar_Scroll(object sender, EventArgs e)
        {
            this.heartbeat = (ushort)((TrackBar)sender).Value;
        }

        public void StartReceiving()
        {
            if (this.ConnectionState != BikeConnectionState.Connected)
            {
                this.sendTimer.Start();
                this.ConnectionState = BikeConnectionState.Connected;
            }
        }

        public void StopReceiving()
        {
            if (this.ConnectionState == BikeConnectionState.Connected)
            {
                this.sendTimer.Stop();
                this.ConnectionState = BikeConnectionState.Disconnected;
            }
        }

        public void SetResistance(int res)
        {
            this.resistance = res;
            this.power = (int)(Math.Min(250, (this.speed / 1000.0) * 12.21) + Math.Min(250, ((this.speed / 1000.0) * 12.21) * this.resistance / 200.0));
        }
    }
}
