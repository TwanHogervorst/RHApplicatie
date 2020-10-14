using ClientApplication.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientApplication.Data
{
    public class BikeDataViewModel
    {
        public ushort HeartBeat { get; set; }
        public double ElapsedTime { get; set; }
        public byte DistanceTraveled { get; set; }
        public double Speed { get; set; }
        public ushort Power { get; set; }


        public BikeDataViewModel(IBikeTrainer bikeTrainer)
        {
            bikeTrainer.BikeDataReceived += BikeTrainer_BikeDataReceived;
        }

        private void BikeTrainer_BikeDataReceived(object sender, BikeDataReceivedEventArgs args)
        {
            switch (args.Type)
            {
                case BikeDataType.HeartBeat:
                    HeartBeat = args.Data.HeartBeat;
                    break;
                case BikeDataType.GeneralFEData:
                    ElapsedTime = (double)args.Data.ElapsedTime / 4;
                    DistanceTraveled = args.Data.DistanceTraveled;
                    Speed = (double)(args.Data.Speed) / 1000;
                    break;
                case BikeDataType.SpecificBikeData:
                    Power = args.Data.Power;
                    break;
            }
            this.OnBikeDataChanged?.Invoke(this, args.Type);
        }
        public event BikeDataChanged OnBikeDataChanged;
    }
    public delegate void BikeDataChanged(BikeDataViewModel sender, BikeDataType type);


}
