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

namespace ClientApplication
{
    public partial class Form1 : Form
    {

        private IBikeTrainer bike;

        public Form1()
        {
            InitializeComponent();

            /*this.bike = new EspBikeTrainer("Avans Bike B69C");
            this.bike.BikeDataReceived += Bike_BikeDataReceived;

            try
            {
                this.bike.StartReceiving();
            }
            catch (BLEException)
            {

            }*/

            Utility.DisableAllChildControls(groupBoxSimulator);

            
        }

        #region Trackbar Events

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
            labelCurrentResistanceValue.Text = textBoxResistance.Text;
        }

        #endregion

        #region TextBox KeyPress Events

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
                    this.bike.SetResistance(trackBarResistance.Value);
                }
                else
                {
                    textBoxResistance.Text = trackBarResistance.Minimum + "";
                    trackBarResistance.Value = trackBarResistance.Minimum;

                    labelCurrentResistanceValue.Text = textBoxResistance.Text;
                    this.bike.SetResistance(trackBarResistance.Value);
                }

            }
        }

        #endregion

        #region RadioButton Events

        private void radioButtonBike_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonBike.Checked)
            {
                radioButtonSimulator.Checked = false;

                textBoxBikeName.Enabled = true;
                buttonConnect.Enabled = true;
            }
            else
            {
                textBoxBikeName.Enabled = false;
                buttonConnect.Enabled = false;

                this.bike?.StopReceiving();
                this.bike = null;

                this.buttonConnect.Text = "Connect";
            }
        }

        private void radioButtonSimulator_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonSimulator.Checked)
            {
                radioButtonBike.Checked = false;

                Utility.EnableAllChildControls(groupBoxSimulator);

                this.bike = new SimulatorBikeTrainer(this.trackBarSpeed, this.trackBarHeartbeat);
                this.bike.BikeDataReceived += Bike_BikeDataReceived;
            }
            else
            {
                Utility.DisableAllChildControls(groupBoxSimulator);
            }
        }

        #endregion

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(this.textBoxBikeName.Text))
            {
                this.bike = new EspBikeTrainer(this.textBoxBikeName.Text);
                this.bike.BikeConnectionChanged += Bike_BikeConnectionChanged;
                this.bike.BikeDataReceived += Bike_BikeDataReceived;

                this.buttonConnect.Text = "Connecting...";
                this.buttonConnect.Enabled = false;

                this.textBoxBikeName.Enabled = false;

                this.bike.StartReceiving();
            }
            else
            {
                MessageBox.Show("Please enter a bike name!", "Missing bike name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.bike?.StopReceiving();
        }

        #region Bike Events

        private void Bike_BikeConnectionChanged(object sender, BikeConnectionStateChangedEventArgs args)
        {
            if(args.ConnectionState == BikeConnectionState.Connected)
            {
                this.buttonConnect.Text = "Connected";
            }
            if(args.ConnectionState == BikeConnectionState.Error)
            {
                MessageBox.Show(args.Exception.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(args.ConnectionState == BikeConnectionState.Error || args.ConnectionState == BikeConnectionState.Disconnected)
            {
                this.bike = null;

                this.textBoxBikeName.Enabled = true;

                this.buttonConnect.Text = "Connect";
                this.buttonConnect.Enabled = true;
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

        #endregion
    }
}
