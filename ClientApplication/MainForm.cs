using ClientApplication.Core;
using ClientApplication.Data;
using ClientApplication.Interface;
using RHApplicationLib.Core;
using ServerUtils;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class MainForm : Form
    {
        private BikeDataViewModel bikeDataViewModel;
        private Client client;
        private VRTunnel tunnel;
        private IBikeTrainer _bike; // DONT USE THIS VARIABLE
        private int currentResistance = 0;
        private int targetResistance = 0;
        private System.Windows.Forms.Timer dataSendTimer;
        private System.Windows.Forms.Timer vrUpdateTimer;
        private Thread vrThread;
        private bool isRunning = false;
        private IBikeTrainer bike
        {
            get => this._bike;
            set
            {
                if (this._bike != null)
                {
                    this._bike.BikeConnectionChanged -= this.Bike_BikeConnectionChanged;
                    this._bike.StopReceiving();
                }
                
                this.dataSendTimer.Stop();
                this._bike = value;

                if (this._bike != null)
                {
                    this.bikeDataViewModel = new BikeDataViewModel(this._bike);
                    this.bikeDataViewModel.OnBikeDataChanged += BikeDataViewModel_OnBikeDataChanged;

                    this._bike.BikeConnectionChanged += Bike_BikeConnectionChanged;

                    this._bike.StartReceiving();

                    this.dataSendTimer.Start();

                    if (this.tunnel != null)
                    {
                        this.vrUpdateTimer.Start();
                    }
                }
            }
        }

        private void BikeDataViewModel_OnBikeDataChanged(BikeDataViewModel sender, BikeDataType type)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                switch (type)
                {
                    case BikeDataType.HeartBeat:
                        labelCurrentHeartbeatValue.Text = sender.HeartBeat.ToString() + " BPM";
                        break;
                    case BikeDataType.GeneralFEData:
                        labelCurrentElapsedTimeValue.Text = sender.ElapsedTime.ToString("0.00") + " s";
                        labelCurrentDistanceTraveledValue.Text = Math.Round(sender.DistanceTraveled, 2).ToString("0.00") + " m";
                        labelCurrentSpeedValue.Text = sender.Speed.ToString("0.00") + " m/s";
                        break;
                    case BikeDataType.SpecificBikeData:
                        labelCurrentPowerValue.Text = sender.Power.ToString() + " W";
                        break;
                }
            });
        }

        public MainForm(Client client, VRTunnel tunnel)
        {
            InitializeComponent();
            dataSendTimer = new System.Windows.Forms.Timer();
            dataSendTimer.Interval = 500;
            dataSendTimer.Tick += DataSendTimer_Tick;
            vrUpdateTimer = new System.Windows.Forms.Timer();
            vrUpdateTimer.Interval = 1000;
            vrUpdateTimer.Tick += VrUpdateTimer_Tick;
            this.client = client;
            this.tunnel = tunnel;
            this.client.OnChatReceived += Client_OnChatReceived;
            this.client.OnResistanceReceived += Client_OnResistanceReceived;
            this.client.OnStartStopSession += Client_OnStartStopSession;
            this.client.OnEmergencyStopSession += Client_OnEmergencyStopSession;

            Utility.DisableAllChildControls(groupBoxSimulator);
        }

        private void VrUpdateTimer_Tick(object sender, EventArgs e)
        {
            if(this.vrThread == null)
            {
                if(this.tunnel != null)
                {
                    this.vrThread = new Thread(UpdateVR);
                    vrThread.Start();
                }
            }else if(this.tunnel != null)
            {
                if (!this.vrThread.IsAlive)
                {
                    this.vrThread = new Thread(UpdateVR);
                    vrThread.Start();
                }
            }
        }

        private void Client_OnStartStopSession(bool state)
        {
            if (this.bike != null)
            {
                if (state)
                {
                    this.Client_OnChatReceived("The session has started\r\n");
                    this.client.SendStartStopSessionResponse(true);
                    this.isRunning = true;
                }
                else
                {
                    this.Client_OnChatReceived("The session has stopped\r\n");
                    this.client.SendStartStopSessionResponse(false);
                    this.isRunning = false;
                }
            } else
            {
                this.client.SendInvalidBike(state);
            }
        }

        private void Client_OnEmergencyStopSession(bool state, int resistance)
        {
            if (this.bike != null)
            {
                if (isRunning)
                {
                    if (!state)
                    {
                        this.Client_OnChatReceived("The session was stopped in emergency!\r\n");
                        this.client.SendEmergencySessionResponse(true);

                        this.currentResistance = 0;
                        this.targetResistance = 0;

                        this.isRunning = false;
                    }
                }
                else
                {
                    this.client.SendEmergencySessionResponse(false);
                }
            }else
            {
                this.client.SendInvalidBike(state);
            }
        }

        private void Client_OnResistanceReceived(int resistance)
        {
            this.targetResistance = resistance;
        }


        private void DataSendTimer_Tick(object sender, EventArgs e)
        {
            this.client.SendData(bikeDataViewModel.Speed, bikeDataViewModel.HeartBeat, bikeDataViewModel.ElapsedTime, bikeDataViewModel.Power, bikeDataViewModel.DistanceTraveled, this.currentResistance);

            if(this.currentResistance != this.targetResistance)
            {
                if (this.targetResistance > this.currentResistance)
                {
                    this.currentResistance++;
                    if (this.targetResistance < this.currentResistance)
                    {
                        this.currentResistance = this.targetResistance;
                    }
                }
                else if (this.targetResistance < this.currentResistance)
                {
                    this.currentResistance--;
                    if (this.targetResistance > this.currentResistance)
                    {
                        this.currentResistance = this.targetResistance;
                    }
                }
            }

            this.Invoke((Action)delegate
            {
                labelCurrentResistanceValue.Text = $"{this.currentResistance / 2.0:0.0} %";
                this.bike.SetResistance((int)this.currentResistance);
                this.bikeDataViewModel.Resistance = this.currentResistance;
            });
        }

        private void UpdateVR()
        {
            tunnel.ClearPanel(tunnel.nodeList["panel"]);
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Bikedata", new decimal[] { 200, 50 }, new decimal(50), new decimal[] { 255, 0, 0, 1 });
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], $"Current Speed: {bikeDataViewModel.Speed}", new decimal[] { 25, 100 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], $"Current Heartbeat: {bikeDataViewModel.HeartBeat}", new decimal[] { 25, 150 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], $"Elapsed Time: {bikeDataViewModel.ElapsedTime}", new decimal[] { 25, 200 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], $"Distance traveled: {bikeDataViewModel.DistanceTraveled}", new decimal[] { 25, 250 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], $"Power: {bikeDataViewModel.Power}", new decimal[] { 25, 300 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], $"Resistance: {this.currentResistance / 2}", new decimal[] { 25, 350 }, new decimal(50));
            tunnel.SwapPanel(tunnel.nodeList["panel"]);

            tunnel.FollowRouteSpeed(tunnel.nodeList["bike"], new decimal(bikeDataViewModel.Speed / 5));
        }

        private void Client_OnChatReceived(string message)
        {
            this.Invoke((Action)delegate
           {
               textBoxChat.Text += message;
               textBoxChat.SelectionStart = textBoxChat.Text.Length;
               textBoxChat.ScrollToCaret();
           });
        }

        private void Client_OnLogin(bool status)
        {
            if (status)
            {
                //turn all groups on, and give dialog that you are succesfully logged in
            }
            else
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
            Application.Exit();
        }

        #region Bike Events

        private void Bike_BikeConnectionChanged(object sender, BikeConnectionStateChangedEventArgs args)
        {
            this.Invoke((Action)delegate () {
                if (args.ConnectionState == BikeConnectionState.Connected)
                {
                    //labelCurrentResistanceValue.Text = $"{trackBarResistance.Value / 2.0:0.0} %";

                    if (sender is EspBikeTrainer) this.buttonConnect.Text = "Connected";
                }
                if (args.ConnectionState == BikeConnectionState.Error)
                {
                    MessageBox.Show(args.Exception.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (args.ConnectionState == BikeConnectionState.Error || args.ConnectionState == BikeConnectionState.Disconnected)
                {
                    this.bike = null;

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
            });
        }

        #endregion

        private void buttonChatSend_Click(object sender, EventArgs e)
        {
            this.Invoke((Action)delegate
            {
                textBoxChat.Text += $"{this.client.username}: {textBoxSendChat.Text}\r\n";
                textBoxChat.SelectionStart = textBoxChat.Text.Length;
                textBoxChat.ScrollToCaret();
            });

            this.client.SendChatMessage(textBoxSendChat.Text);
            textBoxSendChat.Text = "";
        }

        private void buttonChatSend_Click()
        {
            this.Invoke((Action)delegate
            {
                textBoxChat.Text += $"{this.client.username}: {textBoxSendChat.Text}\r\n";
                textBoxChat.SelectionStart = textBoxChat.Text.Length;
                textBoxChat.ScrollToCaret();
            });

            this.client.SendChatMessage(textBoxSendChat.Text);
            textBoxSendChat.Text = "";
        }

        private void textBoxSendChat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonChatSend_Click();
            }
        }
    }
}
