using ClientApplication.Core;
using ClientApplication.Data;
using ClientApplication.Exception;
using ClientApplication.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            IBikeTrainer bike = new EspBikeTrainer("Avans Bike B69C");
            bike.BikeDataReceived += Bike_BikeDataReceived;

            try
            {
                bike.StartReceiving();
            }
            catch (BLEException ex)
            {

            }
        }

        private void Bike_BikeDataReceived(object sender, BikeDataReceivedEventArgs args)
        {
            switch (args.Type)
            {
                case BikeDataType.HeartBeat:
                    this.label1.Invoke((MethodInvoker)delegate { label1.Text = $"Heart rate: {args.Data.HeartBeat} bpm"; });
                    break;
                case BikeDataType.GeneralFEData:
                    {
                        this.label2.Invoke((MethodInvoker)delegate { label2.Text = $"Speed: {args.Data.Speed / 1000.0} m/s"; });
                    }
                    break;
                case BikeDataType.SpecificBikeData:
                    {
                        this.label3.Invoke((MethodInvoker)delegate { label3.Text = $"Power: {args.Data.Power} Watt"; });
                    }
                    break;
            }

            
        }
    }
}
