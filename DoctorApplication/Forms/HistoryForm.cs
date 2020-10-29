using DoctorApplication.Core;
using Newtonsoft.Json;
using RHApplicatieLib;
using RHApplicatieLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

            this.trainingDataDataGridView.AllowUserToAddRows = false;
            this.trainingDataDataGridView.ColumnCount = 7;

            this.trainingDataDataGridView.Columns[0].Name = "Speed";
            this.trainingDataDataGridView.Columns[1].Name = "Heartbeat";
            this.trainingDataDataGridView.Columns[2].Name = "Elapsed Time";
            this.trainingDataDataGridView.Columns[3].Name = "Distance Traveled";
            this.trainingDataDataGridView.Columns[4].Name = "Power";
            this.trainingDataDataGridView.Columns[5].Name = "Resistance";
            this.trainingDataDataGridView.Columns[6].Name = "Timestamp";

            this.client.OnTrainingListReceived += Client_OnTrainingListReceived;
            this.client.OnTrainingDataReceived += Client_OnTrainingDataReceived;

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
            if (forClient == this.selectedUser)
            {
                List<string> doctorList = new List<string>();
                DateTime beginTime = new DateTime();
                DateTime endTime = DateTime.Now;
                double averageSpeed = 0;
                double averageHeartbeat = 0;
                double averagePower = 0;
                double averageResistance = 0;
                double totalDistanceTraveled = 0;
                TimeSpan trainingLength = new TimeSpan();

                List<string[]> rows = new List<string[]>();

                if (trainingData.Count > 0)
                {
                    trainingData.Sort((a, b) => a.timestamp.CompareTo(b.timestamp));

                    beginTime = trainingData.FirstOrDefault().timestamp;

                    foreach (BikeDataPacket dataPacket in trainingData)
                    {
                        if (!doctorList.Contains(dataPacket.doctor) && !string.IsNullOrEmpty(dataPacket.doctor)) doctorList.Add(dataPacket.doctor);
                        averageSpeed += dataPacket.speed;
                        averageHeartbeat += dataPacket.heartbeat;
                        averagePower += dataPacket.power;
                        averageResistance += dataPacket.resistance;

                        rows.Add(new string[] {
                            $"{dataPacket.speed:0.00} m/s",
                            $"{dataPacket.heartbeat} BPM",
                            $"{(dataPacket.timestamp - beginTime).TotalSeconds:0.00} s",
                            $"{Math.Round(dataPacket.distanceTraveled, 2):0.00} m",
                            $"{dataPacket.power} Watt",
                            $"{dataPacket.resistance/2.0:0.0} %",
                            $"{dataPacket.timestamp:H:mm:ss.ff}"
                        });
                    }

                    averageSpeed /= trainingData.Count;
                    averageHeartbeat /= trainingData.Count;
                    averagePower /= trainingData.Count;
                    averageResistance = (averageResistance / 2) / trainingData.Count;
                    totalDistanceTraveled = trainingData.LastOrDefault().distanceTraveled;

                    endTime = trainingData.LastOrDefault().timestamp;

                    trainingLength = endTime - beginTime;
                }

                this.Invoke((Action)delegate ()
                {
                    this.trainingNameValueLabel.Text = trainingName;
                    this.doctorNameValueLabel.Text = string.Join(", ", doctorList);
                    this.trainingStartTimeValueLabel.Text = beginTime.ToString("dd/MM/yyyy H:mm:ss");
                    this.trainingEndTimeValueLabel.Text = endTime.ToString("dd/MM/yyyy H:mm:ss");
                    this.trainingLengthValueLabel.Text = trainingLength.ToString("hh\\:mm\\:ss");

                    this.averageSpeedValueLabel.Text = $"{Math.Round(averageSpeed, 2):0.00} m/s";
                    this.averageHeartbeatValueLabel.Text = $"{Math.Round(averageHeartbeat)} BPM";
                    this.averagePowerValueLabel.Text = $"{Math.Round(averagePower)} Watt";
                    this.averageResistanceValueLabel.Text = $"{Math.Round(averageResistance, 1):0.0} %";
                    this.distanceTraveledValueLabel.Text = $"{Math.Round(totalDistanceTraveled, 2):0.00} m";

                    this.trainingDataDataGridView.Rows.Clear();
                    foreach (string[] rowData in rows)
                    {
                        this.trainingDataDataGridView.Rows.Add(rowData);
                    }
                });
            }
        }

        private void HistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.client.OnTrainingListReceived -= this.Client_OnTrainingListReceived;
            this.client.OnTrainingDataReceived -= this.Client_OnTrainingDataReceived;
        }

        private void HistoryForm_Shown(object sender, EventArgs e)
        {
            this.client.RequestTrainingList(this.selectedUser);
        }
    }
}
