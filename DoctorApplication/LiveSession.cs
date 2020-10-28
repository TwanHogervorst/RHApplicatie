﻿using RHApplicationLib.Core;
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
using ServerUtils;

namespace DoctorApplication
{
    public partial class LiveSession : Form
    {
        private string selectedUser;
        private DoctorClient client;
        private bool IsRunning;
        private DateTime startTimeSession;

        private List<decimal> speedValueList = new List<decimal>();
        private List<decimal> heartbeatValueList = new List<decimal>();
        private List<decimal> resistanceValueList = new List<decimal>();
        private List<decimal> distanceTraveledValueList = new List<decimal>();
        private List<decimal> powerValueList = new List<decimal>();

        public LiveSession(DoctorClient client, String selected)
        {
            InitializeComponent();
            this.client = client;
            this.client.OnChatReceived += Client_OnChatReceived;
            this.client.OnBikeDataReceived += Client_OnBikeDataReceived;
            this.client.OnSessionStateReceived += Client_OnSessionStateReceived;
            this.client.OnSessionStateMessageReceived += Client_OnSessionStateMessageReceived;
            this.client.OnInvalidBikeReceived += Client_OnInvalidBikeReceived;
            this.client.OnEmergencyResponse += Client_OnEmergencyResponse;
            this.selectedUser = selected;
            Patient.Text += selected;

            this.client.RequestSessionState();

            this.speedGraph.MinValue = -1;
            this.speedGraph.MaxValue = 41;

            this.heartbeatGraph.MinValue = 40;
            this.heartbeatGraph.MaxValue = 240;

            this.resistanceGraph.MinValue = -1;
            this.resistanceGraph.MaxValue = 100;

            this.powerGraph.MinValue = -1;
            this.powerGraph.MaxValue = 500;
        }

        private void Client_OnInvalidBikeReceived(string sender)
        {
            IsRunning = false;
            if (sender == selectedUser)
            {
                this.Invoke((Action)delegate
                {
                    textBoxChat.Text += "Bike is not connected!\r\n";
                    textBoxChat.SelectionStart = textBoxChat.Text.Length;
                    textBoxChat.ScrollToCaret();
                    this.client.SendServerMessage(this.selectedUser, "Bike is not connected!\r\n");
                });
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

        private void Client_OnEmergencyResponse(string sender, bool state)
        {
            this.Invoke((Action)delegate
            {
                if (sender == selectedUser)
                {
                    if (state)
                    {
                        textBoxChat.Text += "The session has been stopped in emergency!\r\n";
                        textBoxChat.SelectionStart = textBoxChat.Text.Length;
                        textBoxChat.ScrollToCaret();
                    }
                    else
                    {
                        textBoxChat.Text += "The session was not started!\r\n";
                        textBoxChat.SelectionStart = textBoxChat.Text.Length;
                        textBoxChat.ScrollToCaret();
                    }
                }
            });
        }

        private void Client_OnSessionStateReceived(string clientUserName, DateTime startTimeSession, bool state)
        {
            if (this.selectedUser == clientUserName) {
                this.IsRunning = state;
                this.startTimeSession = startTimeSession;
            }
        }

        private void Client_OnBikeDataReceived(ServerUtils.DataPacket<BikeDataPacket> bikeDataPacket)
        {
            this.Invoke((Action)delegate
            {
                if (this.IsRunning)
                {
                    TimeSpan ts = DateTime.Now - this.startTimeSession;
                    this.labelTimeValue.Text = $"{(int)ts.TotalMinutes:00}:{ts.Seconds:00}";
                }
                this.labelCurrentSpeedValue.Text = bikeDataPacket.data.speed.ToString("0.00") + " m/s";
                this.labelCurrentDistanceValue.Text = Math.Round(bikeDataPacket.data.distanceTraveled, 2).ToString("0.00") + " m";
                this.labelCurrentHearthbeatValue.Text = bikeDataPacket.data.heartbeat.ToString() + " BPM";
                this.labelCurrentPowerValue.Text = bikeDataPacket.data.power.ToString() + " W";
                labelCurrentResistanceValue.Text = (bikeDataPacket.data.resistance / 2.0).ToString("0.0") + " %";

                this.speedValueList.Add((decimal)bikeDataPacket.data.speed);
                this.heartbeatValueList.Add(bikeDataPacket.data.heartbeat);
                this.resistanceValueList.Add(bikeDataPacket.data.resistance / 2.0m);
                this.distanceTraveledValueList.Add((decimal)bikeDataPacket.data.distanceTraveled);
                this.powerValueList.Add(bikeDataPacket.data.power);

                if (this.speedValueList.Count > this.speedGraph.PointsToShow) this.speedValueList.RemoveRange(0, this.speedValueList.Count - this.speedGraph.PointsToShow - 1);
                if (this.heartbeatValueList.Count > this.heartbeatGraph.PointsToShow + 1) this.heartbeatValueList.RemoveRange(0, this.heartbeatValueList.Count - this.heartbeatGraph.PointsToShow - 1);
                if (this.resistanceValueList.Count > this.resistanceGraph.PointsToShow + 1) this.resistanceValueList.RemoveRange(0, this.resistanceValueList.Count - this.resistanceGraph.PointsToShow - 1);
                if (this.powerValueList.Count > this.powerGraph.PointsToShow + 1) this.powerValueList.RemoveRange(0, this.powerValueList.Count - this.powerGraph.PointsToShow - 1);

                this.speedGraph.DataSource = this.speedValueList.ToArray();
                this.heartbeatGraph.DataSource = this.heartbeatValueList.ToArray();
                this.resistanceGraph.DataSource = this.resistanceValueList.ToArray();
                this.distanceTraveledGraph.DataSource = this.distanceTraveledValueList.ToArray();
                this.powerGraph.DataSource = this.powerValueList.ToArray();
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
                this.IsRunning = false;
            }
            else
            {
                this.client.StartSession();
                this.IsRunning = true;
            }

            this.speedValueList.Clear();
            this.heartbeatValueList.Clear();
            this.resistanceValueList.Clear();
            this.distanceTraveledValueList.Clear();
            this.powerValueList.Clear();
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
            this.client.OnEmergencyResponse -= this.Client_OnEmergencyResponse;
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

        private void EmergencyStopButton_Click(object sender, EventArgs e)
        {
            this.client.EmergencyStopSession(selectedUser);
        }
    }
}
