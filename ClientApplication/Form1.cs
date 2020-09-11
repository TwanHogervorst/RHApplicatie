using ClientApplication.Core;
using ClientApplication.Exception;
using ClientApplication.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Avans.TI.BLE;

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
            catch (BLEException)
            {

            }
        }

        private void Bike_BikeDataReceived(object sender, BikeDataReceivedEventArgs args)
        {
            switch (args.Type)
            {
                case BikeDataType.HeartBeat:
                    labelCurrentHeartbeatValue.Invoke((MethodInvoker)delegate () { labelCurrentHeartbeatValue.Text = args.Data.HeartBeat.ToString(); });
                    break;
                case BikeDataType.GeneralFEData:
                    this.Invoke((MethodInvoker)delegate () 
                    {
                        labelCurrentElapsedTimeValue.Text = args.Data.ElapsedTime.ToString();
                        labelCurrentDistanceTraveledValue.Text = args.Data.DistanceTraveled.ToString();
                        labelCurrentSpeedValue.Text = args.Data.Speed.ToString();
                    });
                    break;
                case BikeDataType.SpecificBikeData:
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        labelCurrentPowerValue.Text = args.Data.Power.ToString();
                    });
                    break;
            }
        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxSpeed.Text = "" + (double)(trackBarSpeed.Value)/100.0;
        }

        private void trackBarHeartbeat_Scroll(object sender, EventArgs e)
        {
            textBoxHeartbeat.Text = "" + trackBarHeartbeat.Value;
        }

        private void trackBarResistance_Scroll(object sender, EventArgs e)
        {
            textBoxResistance.Text = "" + trackBarResistance.Value;
        }

        private void textBoxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBoxSpeed.Text))
                {
                    try
                    {
                        string text = textBoxSpeed.Text;
                        if (text.Contains('.')) {
                            text = text.Replace(".",",");
                        }
                        int speedValue = (int)(double.Parse(text) * 100);

                        if (speedValue >= trackBarSpeed.Minimum && speedValue <= trackBarSpeed.Maximum)
                        {
                            trackBarSpeed.Value = speedValue;
                        }
                        else if (speedValue > trackBarSpeed.Maximum)
                        {
                            textBoxSpeed.Text = trackBarSpeed.Maximum + "";
                            textBoxSpeed.Text = textBoxSpeed.Text.Insert(2, ",");
                            trackBarSpeed.Value = trackBarSpeed.Maximum;
                        }
                        else if (speedValue < trackBarSpeed.Minimum)
                        {
                            textBoxSpeed.Text = trackBarSpeed.Minimum + "";
                            textBoxSpeed.Text = textBoxSpeed.Text.Insert(2, ",");
                            trackBarSpeed.Value = trackBarSpeed.Minimum;
                        }
                    }
                    catch
                    {
                        textBoxSpeed.Text = (double)(trackBarSpeed.Value)/100.0 + "";
                        textBoxSpeed.Text = textBoxSpeed.Text.Insert(2, ",");
                    }
                }
                else
                {
                    textBoxSpeed.Text = (double)(trackBarSpeed.Minimum) / 100.0 + "";
                    textBoxSpeed.Text = textBoxSpeed.Text.Insert(2, ",");
                    trackBarSpeed.Value = trackBarSpeed.Minimum;
                }

            }
        }

        private void textBoxHeartbeat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBoxHeartbeat.Text))
                {
                    try
                    {
                        if (int.Parse(textBoxHeartbeat.Text) >= trackBarHeartbeat.Minimum && int.Parse(textBoxHeartbeat.Text) <= trackBarHeartbeat.Maximum)
                        {
                            trackBarHeartbeat.Value = int.Parse(textBoxHeartbeat.Text);
                        }
                        else if (int.Parse(textBoxHeartbeat.Text) > trackBarHeartbeat.Maximum)
                        {
                            textBoxHeartbeat.Text = trackBarHeartbeat.Maximum + "";
                            trackBarHeartbeat.Value = trackBarHeartbeat.Maximum;
                        }
                        else if (int.Parse(textBoxHeartbeat.Text) < trackBarHeartbeat.Minimum)
                        {
                            textBoxHeartbeat.Text = trackBarHeartbeat.Minimum + "";
                            trackBarHeartbeat.Value = trackBarHeartbeat.Minimum;
                        }
                    }
                    catch
                    {
                        textBoxHeartbeat.Text = trackBarHeartbeat.Value + "";

                    }
                }
                else
                {
                    textBoxHeartbeat.Text = trackBarHeartbeat.Minimum + "";
                    trackBarHeartbeat.Value = trackBarHeartbeat.Minimum;
                }

            }
        }

        private void textBoxResistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBoxResistance.Text))
                {
                    try
                    {
                        if (int.Parse(textBoxResistance.Text) >= trackBarResistance.Minimum && int.Parse(textBoxResistance.Text) <= trackBarResistance.Maximum)
                        {
                            trackBarResistance.Value = int.Parse(textBoxResistance.Text);
                        }
                        else if (int.Parse(textBoxResistance.Text) > trackBarResistance.Maximum)
                        {
                            textBoxResistance.Text = trackBarResistance.Maximum + "";
                            trackBarResistance.Value = trackBarResistance.Maximum;
                        }
                        else if (int.Parse(textBoxResistance.Text) < trackBarResistance.Minimum)
                        {
                            textBoxResistance.Text = trackBarResistance.Minimum + "";
                            trackBarResistance.Value = trackBarResistance.Minimum;
                        }
                    }
                    catch
                    {
                        textBoxResistance.Text = trackBarResistance.Value + "";

                    }
                    labelCurrentResistanceValue.Text = textBoxResistance.Text;
                    sendResistance(textBoxResistance.Text);
                }
                else
                {
                    textBoxResistance.Text = trackBarResistance.Minimum + "";
                    trackBarResistance.Value = trackBarResistance.Minimum;
                    labelCurrentResistanceValue.Text = textBoxResistance.Text;
                    sendResistance(textBoxResistance.Text);
                }

            }
        }

        private async Task sendResistance(String value)
        {
            Int32 intValue = Convert.ToInt32(value);

            int errorCode = 0;
            BLE bleBike = new BLE();

            var services = bleBike.GetServices;
            foreach (var service in services)
            {
                Console.WriteLine($"Service: {service}");
            }

            errorCode = await bleBike.SetService("6e40fec1-b5a3-f393-e0a9-e50e24dcca9e"); 

            List<byte> message = new List<byte>() { 0xA4, 0x09, 0x4E, 0x05, 0x30, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, (byte)intValue };

            byte checksum = (byte)message.Aggregate(0, (accu, e) =>
            {
                return accu ^= e;
            });

            message.Add(checksum);

            await bleBike.WriteCharacteristic("6E40FEC3-B5A3-F393-E0A9-E50E24DCCA9E", message.ToArray());
        }

    }
}
