﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class HomePageForm : Form
    {
        private DoctorClient client;

        public HomePageForm(DoctorClient client)
        {
            this.client = client;
            this.client.OnClientListReceived += Client_OnClientListReceived;

            InitializeComponent();

            this.client.RequestClientList();
        }

        private void Client_OnClientListReceived(Dictionary<string, bool> clientList)
        {
            this.Invoke((Action)delegate
            {
                PatientListView.Items.Clear();

                foreach (KeyValuePair<string, bool> client in clientList)
                {
                    PatientListView.Items.Add(client.Key);
                }
            });
        }

        private void LiveSessionButton_Click(object sender, System.EventArgs e)
        {
            LiveSession liveSession = new LiveSession(this.client, selectedUser);
            liveSession.Show();
        }

        private void HistoryButton_Click(object sender, System.EventArgs e)
        {
            HistoryForm historySession = new HistoryForm(selectedUser);
            historySession.Show();
        }

        private string selectedUser = "";

        private void PatientListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListView listview = (ListView)sender;
            selectedUser = listview.SelectedItems[0].Text;
        }
    }
}
