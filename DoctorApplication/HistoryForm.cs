using Newtonsoft.Json;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class HistoryForm : Form
    {
        private string selectedUser;
        private DoctorClient client;

        public HistoryForm(DoctorClient client, string selected)
        {
            InitializeComponent();

            this.selectedUser = selected;
            this.client = client;

            this.client.OnTrainingListReceived += Client_OnTrainingListReceived;
            this.client.OnTrainingDataReceived += Client_OnTrainingDataReceived;

            this.client.RequestTrainingList(this.selectedUser);

            this.trainingListView.SelectedIndexChanged += TrainingListView_SelectedIndexChanged;
        }

        private void TrainingListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.trainingListView.SelectedItems.Count == 1)
            {
                this.client.RequestTrainingData(this.selectedUser, this.trainingListView.SelectedItems[0].Text);
            }
        }

        private void Client_OnTrainingListReceived(string forClient, List<string> trainingList)
        {
            this.Invoke((Action)delegate ()
            {
                if (forClient == this.selectedUser)
                {
                    this.trainingListView.Items.AddRange(trainingList.Select(t => new ListViewItem(t)).ToArray());
                }
            });
        }

        private void Client_OnTrainingDataReceived(string forClient, string trainingName, List<BikeDataPacket> trainingData)
        {
            this.Invoke((Action)delegate ()
            {
                if (forClient == this.selectedUser)
                {
                    this.trainingDataTextBox.Text = JsonConvert.SerializeObject(trainingData, Formatting.Indented);
                }
            });
        }

        private void HistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.client.OnTrainingListReceived -= this.Client_OnTrainingListReceived;
            this.client.OnTrainingDataReceived -= this.Client_OnTrainingDataReceived;
        }
    }
}
