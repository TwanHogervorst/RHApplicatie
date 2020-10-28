using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using ServerUtils;

namespace DoctorApplication
{
    public delegate void UserSelectedCallback(string username, string doctor);
    public partial class PatienListViewUserControl : UserControl
    {
        private DoctorClient client;
        public string selectedUser;

        public event UserSelectedCallback OnUserSelected;

        public PatienListViewUserControl(DoctorClient client, String user)
        {
            InitializeComponent();

            this.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            this.client = client;
            this.client.OnBikeDataReceived += Client_OnBikeDataReceived;

            this.selectedUser = user;
            this.UserControlNameLabel.Text = user;

            // repositioning to center
            this.UserControlNameLabel.Location = new Point((this.Width / 2) - (this.UserControlNameLabel.Width / 2), this.UserControlNameLabel.Location.Y);
            this.label1.Location = new Point((this.Width / 2) - (this.label1.Width / 2), this.label1.Location.Y);
            this.label3.Location = new Point((this.Width / 2) - (this.label3.Width / 2), this.label3.Location.Y);
            this.labelCurrentHearthbeatValue.Location = new Point((this.Width / 2) - (this.labelCurrentHearthbeatValue.Width / 2), this.labelCurrentHearthbeatValue.Location.Y);

        }

        private void Client_OnBikeDataReceived(DataPacket<BikeDataPacket> bikeDataPacket)
        {
            if (bikeDataPacket.sender == UserControlNameLabel.Text)
            {
                this.Invoke((Action)delegate
                {
                    this.labelCurrentHearthbeatValue.Text = bikeDataPacket.data.heartbeat.ToString() + " BPM";
                });
            }
        }

        private void EmergencyShutdownButton_Click(object sender, EventArgs e)
        {
            this.client.EmergencyStopSession(this.selectedUser);
        }

        private void UserControlNameLabel_Click(object sender, EventArgs e)
        {
            OnUserSelected?.Invoke(UserControlNameLabel.Text, this.client.username);
        }
    }
}
