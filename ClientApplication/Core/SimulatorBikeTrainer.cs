using ClientApplication.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ClientApplication.Data;

namespace ClientApplication.Core
{
    public class SimulatorBikeTrainer : IBikeTrainer
    {
        public event BikeDataReceivedEventHandler BikeDataReceived;
        public event BikeConnectionStateChanged BikeConnectionChanged;

        public SimulatorBikeTrainer(TrackBar speedTrackBar, TrackBar bpmTrackBar)
        {
            speedTrackBar.ValueChanged += SpeedTrackBar_Scroll;
        }

        private void SpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            int speed = ((TrackBar)sender).Value;

            DBikeGeneralFEData data = new DBikeGeneralFEData
            {
                Speed = (ushort)speed
            };

            BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.GeneralFEData, data));
        }

        public void StartReceiving()
        {
        }

        public void StopReceiving()
        {
        }
    }
}
