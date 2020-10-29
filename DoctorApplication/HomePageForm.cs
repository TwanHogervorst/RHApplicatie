using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class HomePageForm : Form
    {
        private DoctorClient client;
        private Dictionary<string, bool> clientList;

        public HomePageForm(DoctorClient client)
        {
            this.client = client;
            this.client.OnClientListReceived += Client_OnClientListReceived;
            this.client.OnEmergencyResponse += Client_OnEmergencyResponse;
            InitializeComponent();
            
            this.client.RequestClientList();

            this.PatientTableLayoutPanel.AutoScroll = false;
            this.PatientTableLayoutPanel.HorizontalScroll.Enabled = false;
            this.PatientTableLayoutPanel.HorizontalScroll.Visible = false;
            this.PatientTableLayoutPanel.AutoScroll = true;
        }

        private void Client_OnEmergencyResponse(string sender, bool state)
        {
            if (state)
            {
                MessageBox.Show($"{sender} has been stopped in emergency!", "Emergency stop!");
            }else
            {
                MessageBox.Show("The session was not started", "Error");
            }
        }

        private void Client_OnClientListReceived(Dictionary<string, bool> clientList_)
        {
            this.Invoke((Action)delegate
            {
                this.clientList = clientList_;

                PatientTableLayoutPanel.RowStyles.Clear();
                PatientTableLayoutPanel.ColumnStyles.Clear();
                PatientTableLayoutPanel.Controls.Clear();

                foreach (KeyValuePair<string, bool> userClient in this.clientList)
                {
                    PatienListViewUserControl patientList = new PatienListViewUserControl(client, userClient.Key);
                    patientList.OnPatientListViewItemClick += PatientList_OnUserSelected;
                    PatientTableLayoutPanel.Controls.Add(patientList);
                }

                Func<PatienListViewUserControl, bool> selectPredicate;
                if (!string.IsNullOrEmpty(this.client.selectedUser) && this.clientList.ContainsKey(this.client.selectedUser)) selectPredicate = (plvcu) => plvcu.selectedUser == this.client.selectedUser;
                else selectPredicate = (plvcu) => true;

                this.PatientTableLayoutPanel.Controls.OfType<PatienListViewUserControl>().FirstOrDefault(selectPredicate)?.SelectUser();
            });
        }

        private void PatientList_OnUserSelected(string username)
        {
            foreach (PatienListViewUserControl plvuc in this.PatientTableLayoutPanel.Controls.OfType<PatienListViewUserControl>())
                plvuc.BackColor = SystemColors.Control;

            this.client.selectedUser = username;
            this.CurrentSelectedUserLabel.Text = this.client.selectedUser;
        }

        private void LiveSessionButton_Click(object sender, EventArgs e)
        {
            if (this.client.selectedUser != null && this.clientList[this.client.selectedUser])
            {
                this.client.SendUserName(this.client.selectedUser);
                LiveSession liveSession = new LiveSession(this.client, this.client.selectedUser);
                liveSession.Show();
            }
            else
            {
                MessageBox.Show("No client selected/client is offline.", "Error");
            }
        }

        private void HistoryButton_Click(object sender, EventArgs e)
        {
            if (this.client.selectedUser != null)
            {
                HistoryForm historySession = new HistoryForm(this.client, this.client.selectedUser);
                historySession.Show();
            }
            else
            {
                MessageBox.Show("No client selected.", "Error");
            }
        }

        private void BroadcastSendButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BroadcastTextBox.Text))
            {
                this.client.BroadCast(BroadcastTextBox.Text);
                BroadcastTextBox.Clear();
            }
        }

        private void BroadcastSendButton_Click()
        {
            if (!string.IsNullOrEmpty(BroadcastTextBox.Text))
            {
                this.client.BroadCast(BroadcastTextBox.Text);
                BroadcastTextBox.Clear();
            }
        }

        private void BroadcastTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BroadcastSendButton_Click();
            }
        }

        private void HomePageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.client.DisconnectDoctor();
            Application.Exit();
        }
    }
}
