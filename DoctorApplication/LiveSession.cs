using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public LiveSession(DoctorClient client, String selected)
        {
            InitializeComponent();
            this.client = client;
            this.client.OnChatReceived += Client_OnChatReceived; ;
            this.client.OnBikeDataReceived += Client_OnBikeDataReceived;
            this.selectedUser = selected;
            Patient.Text += selected;
        }

        private void Client_OnBikeDataReceived(ServerUtils.BikeDataPacket bikeDataPacket)
        {
            this.Invoke((Action)delegate
            {
                this.labelCurrentSpeedValue.Text = bikeDataPacket.speed.ToString("0.00") + " m/s";
                this.labelCurrentDistanceValue.Text = bikeDataPacket.distanceTraveled.ToString() + " m";
                this.labelCurrentHearthbeatValue.Text = bikeDataPacket.heartbeat.ToString() + " BPM";
                this.labelCurrentPowerValue.Text = bikeDataPacket.power.ToString() + " W";
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

        private void LiveSession_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.client.OnCloseLiveSession();
            this.client.OnChatReceived -= this.Client_OnChatReceived;
        }
    }
}
