using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApplication.Interface
{

    public delegate void BikeDataReceivedEventHandler(object sender, BikeDataReceivedEventArgs args);

    public interface IBikeTrainer
    {

        public event BikeDataReceivedEventHandler BikeDataReceived;

        public void StartReceiving();

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

    public enum BikeDataType
    {
        HeartBeat,
        GeneralFEData,
        SpecificBikeData
    }
}
