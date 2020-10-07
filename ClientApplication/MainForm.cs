using ClientApplication.Core;
using ClientApplication.Interface;
using RHApplicationLib.Core;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class MainForm : Form
    {

        private Client client;
        private IBikeTrainer _bike; // DONT USE THIS VARIABLE
        private IBikeTrainer bike
        {
            get => this._bike;
            set
            {
                this._bike?.StopReceiving();
                this._bike = value;

                if (this._bike != null)
                {
                    this._bike.BikeDataReceived += Bike_BikeDataReceived;
                    this._bike.BikeConnectionChanged += Bike_BikeConnectionChanged;
                    this._bike.StartReceiving();
                }
            }
        }

        public MainForm(Client client)
        {
            InitializeComponent();
            this.client = client;

            this.client.OnChatReceived += Client_OnChatReceived;

            Utility.DisableAllChildControls(groupBoxSimulator);

            this.textBoxResistance.Enabled = false;
            this.trackBarResistance.Enabled = false;
        }

        private void Client_OnChatReceived(string message)
        {
            //add message to chatTextBox
        }

        private void Client_OnLogin(bool status)
        {
            if (status)
            {
                //turn all groups on, and give dialog that you are succesfully logged in
            }else
            {
                //give dialog that logged in was unsuccesfull
            }
        }

        #region Trackbar Events

        private void trackBarSpeed_Changed(object sender, EventArgs e)
        {
            textBoxSpeed.Text = (trackBarSpeed.Value / 100.0).ToString("0.00");
        }

        private void trackBarHeartbeat_Changed(object sender, EventArgs e)
        {
            textBoxHeartbeat.Text = trackBarHeartbeat.Value.ToString();
        }

        private void trackBarResistance_Changed(object sender, EventArgs e)
        {
            textBoxResistance.Text = (trackBarResistance.Value / 2.0).ToString("0.0");
            labelCurrentResistanceValue.Text = $"{trackBarResistance.Value / 2.0:0.0} %";

            this.bike.SetResistance(trackBarResistance.Value);
        }

        #endregion

        #region TextBox KeyPress Events

        private void textBoxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int speedValue = trackBarSpeed.Minimum;

                if (!string.IsNullOrWhiteSpace(textBoxSpeed.Text))
                {
                    try
                    {
                        speedValue = (int)Utility.Bound(
                            (decimal)(double.Parse(textBoxSpeed.Text.Replace(',', '.'), CultureInfo.InvariantCulture) * 100),
                            trackBarSpeed.Minimum,
                            trackBarSpeed.Maximum);
                    }
                    catch
                    {
                        speedValue = trackBarSpeed.Value;
                    }
                }

                textBoxSpeed.Text = (speedValue / 100.0).ToString("0.00");
                trackBarSpeed.Value = speedValue;
            }
        }

        private void textBoxHeartbeat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int heartbeat = trackBarHeartbeat.Minimum;

                if (!string.IsNullOrEmpty(textBoxHeartbeat.Text))
                {
                    try
                    {
                        heartbeat = (int)Utility.Bound(int.Parse(textBoxHeartbeat.Text), trackBarHeartbeat.Minimum, trackBarHeartbeat.Maximum);
                    }
                    catch
                    {
                        textBoxHeartbeat.Text = trackBarHeartbeat.Value.ToString();
                    }
                }

                textBoxHeartbeat.Text = heartbeat.ToString();
                trackBarHeartbeat.Value = heartbeat;

            }
        }

        private void textBoxResistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int resistance = trackBarResistance.Value;

                if (!string.IsNullOrEmpty(textBoxResistance.Text))
                {
                    try
                    {
                        resistance = (int)Utility.Bound(
                            (decimal)(double.Parse(textBoxResistance.Text.Replace(',', '.'), CultureInfo.InvariantCulture) * 2),
                            trackBarResistance.Minimum,
                            trackBarResistance.Maximum);
                    }
                    catch
                    {
                        resistance = trackBarResistance.Value;
                    }
                }

                textBoxResistance.Text = (resistance / 2.0).ToString("0.0");
                trackBarResistance.Value = resistance;

                this.bike.SetResistance(resistance);
                labelCurrentResistanceValue.Text = double.Parse(textBoxResistance.Text) / 2 + " %";
            }
        }

        #endregion

        #region RadioButton Events

        private void radioButtonBike_CheckedChanged(object sender, EventArgs e)
        {
            this.bike = null;

            if (radioButtonBike.Checked)
            {
                radioButtonSimulator.Checked = false;

                textBoxBikeName.Enabled = true;
                buttonConnect.Enabled = true;
            }
            else
            {
                textBoxBikeName.Enabled = false;
                buttonConnect.Enabled = false;

                this.buttonConnect.Text = "Connect";
            }
        }

        private void radioButtonSimulator_CheckedChanged(object sender, EventArgs e)
        {
            this.bike = null;

            if (radioButtonSimulator.Checked)
            {
                radioButtonBike.Checked = false;

                Utility.EnableAllChildControls(groupBoxSimulator);

                this.bike = new SimulatorBikeTrainer(this.trackBarSpeed, this.trackBarHeartbeat);
            }
            else
            {
                Utility.DisableAllChildControls(groupBoxSimulator);
            }
        }

        #endregion

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBoxBikeName.Text))
            {
                this.bike = new EspBikeTrainer(this.textBoxBikeName.Text);

                this.buttonConnect.Text = "Connecting...";
                this.buttonConnect.Enabled = false;

                this.textBoxBikeName.Enabled = false;
            }
            else
            {
                MessageBox.Show("Please enter a bike name!", "Missing bike name", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.bike = null; // kills the connection
        }

        #region Bike Events

        private void Bike_BikeConnectionChanged(object sender, BikeConnectionStateChangedEventArgs args)
        {
            if (args.ConnectionState == BikeConnectionState.Connected)
            {
                this.textBoxResistance.Enabled = true;
                this.trackBarResistance.Enabled = true;
                this.trackBarResistance.Value = 0;

                labelCurrentResistanceValue.Text = $"{trackBarResistance.Value / 2.0:0.0} %";

                if (sender is EspBikeTrainer) this.buttonConnect.Text = "Connected";
            }
            if (args.ConnectionState == BikeConnectionState.Error)
            {
                MessageBox.Show(args.Exception.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (args.ConnectionState == BikeConnectionState.Error || args.ConnectionState == BikeConnectionState.Disconnected)
            {
                this.bike = null;

                this.textBoxResistance.Enabled = false;
                this.trackBarResistance.Enabled = false;

                // reset values for data labels
                foreach (Control control in this.groupBoxBikeData.Controls)
                {
                    if (control is Label && control.Name.EndsWith("Value")) control.Text = "waiting for value";
                }

                if (sender is EspBikeTrainer)
                {
                    this.textBoxBikeName.Enabled = true;

                    this.buttonConnect.Text = "Connect";
                    this.buttonConnect.Enabled = true;
                }
            }
        }

        private void Bike_BikeDataReceived(object sender, BikeDataReceivedEventArgs args)
        {
            switch (args.Type)
            {
                case BikeDataType.HeartBeat:
                    labelCurrentHeartbeatValue.Invoke((MethodInvoker)delegate ()
                    {
                        labelCurrentHeartbeatValue.Text = args.Data.HeartBeat.ToString() + " BPM";
                    });
                    break;
                case BikeDataType.GeneralFEData:
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        labelCurrentElapsedTimeValue.Text = (((double)args.Data.ElapsedTime / 4)).ToString("0.00") + " s";
                        labelCurrentDistanceTraveledValue.Text = args.Data.DistanceTraveled.ToString() + " m";
                        labelCurrentSpeedValue.Text = ((double)(args.Data.Speed) / 1000).ToString("0.00") + " m/s";
                    });
                    break;
                case BikeDataType.SpecificBikeData:
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        labelCurrentPowerValue.Text = args.Data.Power.ToString() + " W";
                    });
                    break;
            }
        }

        #endregion

        private void buttonChatSend_Click(object sender, EventArgs e)
        {
            //TODO
            //this.client.SendData(TextBoxChat.Text);
        }


        private void labelCurrentSpeedText_Click(object sender, EventArgs e)
        {

        }
    }
}
