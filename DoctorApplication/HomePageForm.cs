using System;
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
                PatientTableLayoutPanel.RowStyles.Clear();
                PatientTableLayoutPanel.ColumnStyles.Clear();

                foreach (KeyValuePair<string, bool> userClient in clientList)
                {
                    PatientTableLayoutPanel.Controls.Add(new PatienListViewUserControl(client, userClient.Key));
                }
            });
        }

        private void LiveSessionButton_Click(object sender, System.EventArgs e)
        {
            this.client.SendUserName(selectedUser);
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
            if (listview.SelectedItems.Count > 0) {
                selectedUser = listview.SelectedItems[0].Text;
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
    }
}
