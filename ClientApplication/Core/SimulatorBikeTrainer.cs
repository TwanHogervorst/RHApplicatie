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

        private double speed;
        private double time;
        private double distance;
        private Timer timerElapsedTime;

        public SimulatorBikeTrainer(TrackBar speedTrackBar, TrackBar bpmTrackBar)
        {
            speedTrackBar.ValueChanged += SpeedTrackBar_Scroll;
            bpmTrackBar.ValueChanged += HeartBeatTrackBar_Scroll;

            timerElapsedTime = new Timer();
            timerElapsedTime.Interval = 250;
            timerElapsedTime.Tick += new System.EventHandler(this.timerElapsedTime_Tick);

            speed = speedTrackBar.Value * 10;
            time = 0.0;
            distance = 0.0;
        }

        private void timerElapsedTime_Tick(object sender, EventArgs e)
        {
            time += 0.25;
            distance = (0.25 * speed/1000) + distance;

            DBikeGeneralFEData data = new DBikeGeneralFEData
            {
                Speed = (ushort)speed,
                ElapsedTime = (byte)(time * 4),
                DistanceTraveled = (byte)distance,
            };

            BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.GeneralFEData, data));
        }

        private void SpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            speed = ((TrackBar)sender).Value * 10;
            

            DBikeGeneralFEData data = new DBikeGeneralFEData
            {
                Speed = (ushort)speed,
                ElapsedTime = (byte) time,
                DistanceTraveled = (byte) distance,
            };

            BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.GeneralFEData, data));
        }

        private void HeartBeatTrackBar_Scroll(object sender, EventArgs e)
        {
            int bpm = ((TrackBar)sender).Value;

            DBikeHeartBeat data = new DBikeHeartBeat
            {
                HeartBeat = (ushort)bpm
            };

            BikeDataReceived?.Invoke(this, new BikeDataReceivedEventArgs(BikeDataType.HeartBeat, data));
        }

        public void StartReceiving()
        {
            timerElapsedTime.Start();
        }

        public void StopReceiving()
        {
            timerElapsedTime.Stop();
        }

        public void SetResistance(int resistance)
        {
        }
    }
}
