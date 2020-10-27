﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class PatienListViewUserControl : UserControl
    {
        private DoctorClient client;
        private bool IsRunning;

        public PatienListViewUserControl(DoctorClient client, String user)
        {
            InitializeComponent();
            this.client = client;
            this.client.OnBikeDataReceived += Client_OnBikeDataReceived;
            this.UserControlNameLabel.Text = user;
            this.UserControlNameLabel.Location = new System.Drawing.Point((this.Width / 2) - (this.UserControlNameLabel.Width / 2), this.UserControlNameLabel.Location.Y);
            this.label1.Location = new System.Drawing.Point((this.Width / 2) - (this.label1.Width / 2), this.label1.Location.Y);
            this.label3.Location = new System.Drawing.Point((this.Width / 2) - (this.label3.Width / 2), this.label3.Location.Y);
            this.labelCurrentHearthbeatValue.Location = new System.Drawing.Point((this.Width / 2) - (this.labelCurrentHearthbeatValue.Width / 2), this.labelCurrentHearthbeatValue.Location.Y);

        }

        private void Client_OnBikeDataReceived(ServerUtils.BikeDataPacket bikeDataPacket)
        {
            this.Invoke((Action)delegate
            {
                this.labelCurrentHearthbeatValue.Text = bikeDataPacket.heartbeat.ToString() + " BPM";
            });
        }

        private void EmergencyShutdownButton_Click(object sender, EventArgs e)
        {

        }

        private void PatienListViewUserControl_Load(object sender, EventArgs e)
        {
            this.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        }
    }
}