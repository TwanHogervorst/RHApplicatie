using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using ServerUtils;
using System.Linq;

namespace DoctorApplication
{
    public delegate void UserSelectedCallback(string username);
    public partial class PatienListViewUserControl : UserControl
    {
        private DoctorClient client;
        public string selectedUser;

        public event UserSelectedCallback OnPatientListViewItemClick;

        public PatienListViewUserControl(DoctorClient client, string user)
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

            this.Click += this.PatientListViewUserControl_Click;

            foreach(Control control in this.Controls.OfType<Control>().Where(c => c != this.EmergencyShutdownButton))
                control.Click += this.PatientListViewUserControl_Click;
        }

        public void SelectUser()
        {
            this.PatientListViewUserControl_Click(this, new EventArgs());
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

        private void PatientListViewUserControl_Click(object sender, EventArgs e)
        {
            OnPatientListViewItemClick?.Invoke(UserControlNameLabel.Text);

            this.BackColor = SystemColors.ControlLight;
        }

        private void PatienListViewUserControl_Resize(object sender, EventArgs e)
        {
            // repositioning to center
            this.UserControlNameLabel.Location = new Point((this.Width / 2) - (this.UserControlNameLabel.Width / 2), this.UserControlNameLabel.Location.Y);
            this.label1.Location = new Point((this.Width / 2) - (this.label1.Width / 2), this.label1.Location.Y);
            this.label3.Location = new Point((this.Width / 2) - (this.label3.Width / 2), this.label3.Location.Y);
            this.labelCurrentHearthbeatValue.Location = new Point((this.Width / 2) - (this.labelCurrentHearthbeatValue.Width / 2), this.labelCurrentHearthbeatValue.Location.Y);
        }
    }
}
