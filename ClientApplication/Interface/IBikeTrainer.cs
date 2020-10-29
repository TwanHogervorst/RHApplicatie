using ClientApplication.Exception;
using RHApplicatieLib.Abstract;

namespace ClientApplication.Interface
{

    public delegate void BikeDataReceivedEventHandler(object sender, BikeDataReceivedEventArgs args);
    public delegate void BikeConnectionStateChanged(object sender, BikeConnectionStateChangedEventArgs args);

    public interface IBikeTrainer
    {

        public event BikeDataReceivedEventHandler BikeDataReceived;
        public event BikeConnectionStateChanged BikeConnectionChanged;

        public BikeConnectionState ConnectionState { get; }

        public void StartReceiving();

        public void StopReceiving();

        public void SetResistance(int resistance);

    }

    public class BikeDataReceivedEventArgs
    {
        public BikeDataType Type { get; private set; }
        public dynamic Data { get; private set; } // use dynamic so you won't need to cast. Base type is always DAbstract
                                                  // dynamic type can be found using BikeDataReceivedEventArgs.Type

        public BikeDataReceivedEventArgs(BikeDataType type, DAbstract data)
        {
            this.Type = type;
            this.Data = data;
        }
    }

    public class BikeConnectionStateChangedEventArgs
    {
        public BikeConnectionState ConnectionState { get; private set; }
        public BLEException Exception { get; private set; }

        public BikeConnectionStateChangedEventArgs(BikeConnectionState state, BLEException exception = null)
        {
            this.ConnectionState = state;
            this.Exception = exception;
        }
    }

    public enum BikeDataType
    {
        HeartBeat,
        GeneralFEData,
        SpecificBikeData
    }

    public enum BikeConnectionState
    {
        Disconnected,
        Connected,
        Error
    }
}
