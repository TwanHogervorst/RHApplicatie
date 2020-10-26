using RHApplicationLib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class LiveSession : Form
    {
        private string selectedUser;
        private DoctorClient client;
        private bool IsRunning;
        private DateTime startTimeSession;
        public LiveSession(DoctorClient client, String selected)
        {
            InitializeComponent();
            this.client = client;
            this.client.OnChatReceived += Client_OnChatReceived; ;
            this.client.OnBikeDataReceived += Client_OnBikeDataReceived;
            this.client.OnSessionStateReceived += Client_OnSessionStateReceived;
            this.client.OnSessionStateMessageReceived += Client_OnSessionStateMessageReceived;
            this.client.OnInvalidBikeReceived += Client_OnInvalidBikeReceived;
            this.selectedUser = selected;
            Patient.Text += selected;

           this.client.RequestSessionState();
        }

        private void Client_OnInvalidBikeReceived(string sender)
        {
            if (sender == selectedUser)
            {
                if (this.IsRunning)
                {
                    this.Invoke((Action)delegate
                    {
                        textBoxChat.Text += "Bike is not connected!\r\n";
                    textBoxChat.SelectionStart = textBoxChat.Text.Length;
                    textBoxChat.ScrollToCaret();

                    this.client.SendServerMessage(this.selectedUser,"Bike is not connected!");
                    });
                }
                this.IsRunning = false;
            }
        }

        private void Client_OnSessionStateMessageReceived(string sender, bool state)
        {
            if (state)
            {
                this.Invoke((Action)delegate
                {
                    if (sender == selectedUser)
                    {
                        textBoxChat.Text += "The session has started!\r\n";
                        textBoxChat.SelectionStart = textBoxChat.Text.Length;
                        textBoxChat.ScrollToCaret();
                    }
                });
            }
            else
            {
                this.Invoke((Action)delegate
                {
                    if (sender == selectedUser)
                    {
                        textBoxChat.Text += "The session has stopped!\r\n";
                        textBoxChat.SelectionStart = textBoxChat.Text.Length;
                        textBoxChat.ScrollToCaret();
                    }
                });
            }
        }

        private void Client_OnSessionStateReceived(string clientUserName, DateTime startTimeSession, bool state)
        {
            if (this.selectedUser == clientUserName) {
                this.IsRunning = state;
                this.startTimeSession = startTimeSession;
            }
        }

        private void Client_OnBikeDataReceived(ServerUtils.BikeDataPacket bikeDataPacket)
        {
            this.Invoke((Action)delegate
            {
                if (this.IsRunning)
                {
                    TimeSpan ts = DateTime.Now - this.startTimeSession;
                    this.labelTimeValue.Text = $"{(int)ts.TotalMinutes:00}:{ts.Seconds:00}";
                }
               
                this.labelCurrentSpeedValue.Text = bikeDataPacket.speed.ToString("0.00") + " m/s";
                this.labelCurrentDistanceValue.Text = bikeDataPacket.distanceTraveled.ToString() + " m";
                this.labelCurrentHearthbeatValue.Text = bikeDataPacket.heartbeat.ToString() + " BPM";
                this.labelCurrentPowerValue.Text = bikeDataPacket.power.ToString() + " W";
                labelCurrentResistanceValue.Text = bikeDataPacket.resistance.ToString() + " %";
            });
        }

        private void Client_OnChatReceived(string sender, string message)
        {
            this.Invoke((Action)delegate
            {
                if (sender == selectedUser)
                {
                    textBoxChat.Text += message;
                    textBoxChat.SelectionStart = textBoxChat.Text.Length;
                    textBoxChat.ScrollToCaret();
                }
            });
        }

        private void labelCurrentDistance_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            //TODO Start/Stop method
            if (this.IsRunning)
            {
                this.client.StopSession();
            }
            else
            {
                this.client.StartSession();
            }
        }

        private void speedGraph_Click(object sender, EventArgs e)
        {
            
        }

        private void LiveSession_Load(object sender, EventArgs e)
        {

        }

        private void buttonSendChat_Click(object sender, EventArgs e)
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

        private void buttonSendChat_Click()
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

        private void LiveSession_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.client.OnCloseLiveSession();
            this.client.OnChatReceived -= this.Client_OnChatReceived;
            this.client.OnSessionStateReceived -= this.Client_OnSessionStateReceived;
            this.client.OnBikeDataReceived -= this.Client_OnBikeDataReceived;
            this.client.OnSessionStateMessageReceived -= this.Client_OnSessionStateMessageReceived;
            this.client.OnInvalidBikeReceived -= this.Client_OnInvalidBikeReceived;
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

                //labelCurrentResistanceValue.Text = double.Parse(textBoxResistance.Text) / 2 + " %";

                this.client.SendResistance(resistance);
            }
        }

        private void trackBarResistance_Changed(object sender, EventArgs e)
        {
            textBoxResistance.Text = (trackBarResistance.Value / 2.0).ToString("0.0");
            //labelCurrentResistanceValue.Text = $"{trackBarResistance.Value / 2.0:0.0} %";

            this.client.SendResistance(trackBarResistance.Value);
        }

        private void textBoxSendChat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                buttonSendChat_Click();
            }
        }
    }
}
