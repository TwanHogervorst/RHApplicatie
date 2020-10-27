using ClientApplication.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace ClientApplication.Data
{
    public class BikeDataViewModel
    {
        private DateTime previousTimestamp = new DateTime();

        public ushort HeartBeat { get; set; }
        public double ElapsedTime { get; set; }
        public double DistanceTraveled { get; set; }
        public double Speed { get; set; }
        public ushort Power { get; set; }
        public int Resistance { get; set; }


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
                    Speed = (double)(args.Data.Speed) / 1000;

                    DateTime current = DateTime.Now;
                    if (this.previousTimestamp.Ticks != 0)
                    {
                        DistanceTraveled += this.Speed * (current - previousTimestamp).TotalSeconds;
                    }
                    previousTimestamp = current;

                    break;
                case BikeDataType.SpecificBikeData:
                    Power = args.Data.Power;
                    break;
            }
            this.OnBikeDataChanged?.Invoke(this, args.Type);
        }

        public void ResetDistanceTraveled()
        {
            this.previousTimestamp = new DateTime();
            this.DistanceTraveled = 0;
        }

        public event BikeDataChanged OnBikeDataChanged;
    }
    public delegate void BikeDataChanged(BikeDataViewModel sender, BikeDataType type);


}
