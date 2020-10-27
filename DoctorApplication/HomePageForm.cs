﻿using System;
using System.Collections.Generic;
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

            InitializeComponent();

            this.client.RequestClientList();
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
                        patientList.OnUserSelected += PatientList_OnUserSelected;
                        PatientTableLayoutPanel.Controls.Add(patientList);
                }
            });
        }

        private void PatientList_OnUserSelected(string username)
        {
            this.selectedUser = username;
            CurrentSelectedUserLabel.Text = this.selectedUser;
        }

        private void LiveSessionButton_Click(object sender, System.EventArgs e)
        {
            if (this.selectedUser != "" && this.clientList[this.selectedUser])
            {
                this.client.SendUserName(selectedUser);
                LiveSession liveSession = new LiveSession(this.client, selectedUser);
                liveSession.Show();
            }
            else
            {
                MessageBox.Show("No client selected/client is offline.", "Error");
            }
        }

        private void HistoryButton_Click(object sender, System.EventArgs e)
        {
            if (this.selectedUser != "")
            {
                HistoryForm historySession = new HistoryForm(selectedUser);
                historySession.Show();
            }
            else
            {
                MessageBox.Show("No client selected.", "Error");
            }
        }

        private string selectedUser = "";

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
    }
}
