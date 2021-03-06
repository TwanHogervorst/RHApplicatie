﻿namespace DoctorApplication
{
    partial class LiveSession
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Patient = new System.Windows.Forms.Label();
            this.textBoxSendChat = new System.Windows.Forms.TextBox();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.buttonSendChat = new System.Windows.Forms.Button();
            this.labelCurrentSpeed = new System.Windows.Forms.Label();
            this.labelCurrentSpeedValue = new System.Windows.Forms.Label();
            this.labelCurrentHearthbeat = new System.Windows.Forms.Label();
            this.labelCurrentHearthbeatValue = new System.Windows.Forms.Label();
            this.labelCurrentResistance = new System.Windows.Forms.Label();
            this.labelCurrentResistanceValue = new System.Windows.Forms.Label();
            this.labelCurrentDistance = new System.Windows.Forms.Label();
            this.labelCurrentDistanceValue = new System.Windows.Forms.Label();
            this.labelCurrentPower = new System.Windows.Forms.Label();
            this.labelCurrentPowerValue = new System.Windows.Forms.Label();
            this.groupBoxCurrentData = new System.Windows.Forms.GroupBox();
            this.powerGraph = new DoctorApplication.GraphControl();
            this.distanceTraveledGraph = new DoctorApplication.GraphControl();
            this.resistanceGraph = new DoctorApplication.GraphControl();
            this.heartbeatGraph = new DoctorApplication.GraphControl();
            this.speedGraph = new DoctorApplication.GraphControl();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.EmergencyStopButton = new System.Windows.Forms.Button();
            this.labelRestistance = new System.Windows.Forms.Label();
            this.textBoxResistance = new System.Windows.Forms.TextBox();
            this.trackBarResistance = new System.Windows.Forms.TrackBar();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelTimeValue = new System.Windows.Forms.Label();
            this.groupBoxCurrentData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).BeginInit();
            this.SuspendLayout();
            // 
            // Patient
            // 
            this.Patient.AutoSize = true;
            this.Patient.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Patient.Location = new System.Drawing.Point(14, 10);
            this.Patient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Patient.Name = "Patient";
            this.Patient.Size = new System.Drawing.Size(91, 25);
            this.Patient.TabIndex = 0;
            this.Patient.Text = "Patient: ";
            // 
            // textBoxSendChat
            // 
            this.textBoxSendChat.Location = new System.Drawing.Point(643, 654);
            this.textBoxSendChat.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSendChat.Name = "textBoxSendChat";
            this.textBoxSendChat.Size = new System.Drawing.Size(182, 23);
            this.textBoxSendChat.TabIndex = 1;
            this.textBoxSendChat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSendChat_KeyPress);
            // 
            // textBoxChat
            // 
            this.textBoxChat.Location = new System.Drawing.Point(643, 94);
            this.textBoxChat.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.ReadOnly = true;
            this.textBoxChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxChat.Size = new System.Drawing.Size(276, 548);
            this.textBoxChat.TabIndex = 2;
            // 
            // buttonSendChat
            // 
            this.buttonSendChat.Location = new System.Drawing.Point(832, 651);
            this.buttonSendChat.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSendChat.Name = "buttonSendChat";
            this.buttonSendChat.Size = new System.Drawing.Size(88, 26);
            this.buttonSendChat.TabIndex = 3;
            this.buttonSendChat.Text = "Send";
            this.buttonSendChat.UseVisualStyleBackColor = true;
            this.buttonSendChat.Click += new System.EventHandler(this.buttonSendChat_Click);
            // 
            // labelCurrentSpeed
            // 
            this.labelCurrentSpeed.AutoSize = true;
            this.labelCurrentSpeed.Location = new System.Drawing.Point(270, 51);
            this.labelCurrentSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentSpeed.Name = "labelCurrentSpeed";
            this.labelCurrentSpeed.Size = new System.Drawing.Size(88, 15);
            this.labelCurrentSpeed.TabIndex = 4;
            this.labelCurrentSpeed.Text = "Current Speed: ";
            // 
            // labelCurrentSpeedValue
            // 
            this.labelCurrentSpeedValue.AutoSize = true;
            this.labelCurrentSpeedValue.Location = new System.Drawing.Point(438, 51);
            this.labelCurrentSpeedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentSpeedValue.Name = "labelCurrentSpeedValue";
            this.labelCurrentSpeedValue.Size = new System.Drawing.Size(97, 15);
            this.labelCurrentSpeedValue.TabIndex = 5;
            this.labelCurrentSpeedValue.Text = "Waiting for value";
            // 
            // labelCurrentHearthbeat
            // 
            this.labelCurrentHearthbeat.AutoSize = true;
            this.labelCurrentHearthbeat.Location = new System.Drawing.Point(270, 172);
            this.labelCurrentHearthbeat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentHearthbeat.Name = "labelCurrentHearthbeat";
            this.labelCurrentHearthbeat.Size = new System.Drawing.Size(108, 15);
            this.labelCurrentHearthbeat.TabIndex = 6;
            this.labelCurrentHearthbeat.Text = "Current Heartbeat: ";
            // 
            // labelCurrentHearthbeatValue
            // 
            this.labelCurrentHearthbeatValue.AutoSize = true;
            this.labelCurrentHearthbeatValue.Location = new System.Drawing.Point(438, 172);
            this.labelCurrentHearthbeatValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentHearthbeatValue.Name = "labelCurrentHearthbeatValue";
            this.labelCurrentHearthbeatValue.Size = new System.Drawing.Size(97, 15);
            this.labelCurrentHearthbeatValue.TabIndex = 7;
            this.labelCurrentHearthbeatValue.Text = "Waiting for value";
            // 
            // labelCurrentResistance
            // 
            this.labelCurrentResistance.AutoSize = true;
            this.labelCurrentResistance.Location = new System.Drawing.Point(270, 281);
            this.labelCurrentResistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentResistance.Name = "labelCurrentResistance";
            this.labelCurrentResistance.Size = new System.Drawing.Size(111, 15);
            this.labelCurrentResistance.TabIndex = 8;
            this.labelCurrentResistance.Text = "Current Resistance: ";
            // 
            // labelCurrentResistanceValue
            // 
            this.labelCurrentResistanceValue.AutoSize = true;
            this.labelCurrentResistanceValue.Location = new System.Drawing.Point(438, 281);
            this.labelCurrentResistanceValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentResistanceValue.Name = "labelCurrentResistanceValue";
            this.labelCurrentResistanceValue.Size = new System.Drawing.Size(97, 15);
            this.labelCurrentResistanceValue.TabIndex = 9;
            this.labelCurrentResistanceValue.Text = "Waiting for value";
            // 
            // labelCurrentDistance
            // 
            this.labelCurrentDistance.AutoSize = true;
            this.labelCurrentDistance.Location = new System.Drawing.Point(270, 400);
            this.labelCurrentDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentDistance.Name = "labelCurrentDistance";
            this.labelCurrentDistance.Size = new System.Drawing.Size(147, 15);
            this.labelCurrentDistance.TabIndex = 10;
            this.labelCurrentDistance.Text = "Current Distance Traveled: ";
            this.labelCurrentDistance.Click += new System.EventHandler(this.labelCurrentDistance_Click);
            // 
            // labelCurrentDistanceValue
            // 
            this.labelCurrentDistanceValue.AutoSize = true;
            this.labelCurrentDistanceValue.Location = new System.Drawing.Point(438, 400);
            this.labelCurrentDistanceValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentDistanceValue.Name = "labelCurrentDistanceValue";
            this.labelCurrentDistanceValue.Size = new System.Drawing.Size(97, 15);
            this.labelCurrentDistanceValue.TabIndex = 11;
            this.labelCurrentDistanceValue.Text = "Waiting for value";
            // 
            // labelCurrentPower
            // 
            this.labelCurrentPower.AutoSize = true;
            this.labelCurrentPower.Location = new System.Drawing.Point(270, 514);
            this.labelCurrentPower.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentPower.Name = "labelCurrentPower";
            this.labelCurrentPower.Size = new System.Drawing.Size(89, 15);
            this.labelCurrentPower.TabIndex = 12;
            this.labelCurrentPower.Text = "Current Power: ";
            this.labelCurrentPower.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelCurrentPowerValue
            // 
            this.labelCurrentPowerValue.AutoSize = true;
            this.labelCurrentPowerValue.Location = new System.Drawing.Point(438, 514);
            this.labelCurrentPowerValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentPowerValue.Name = "labelCurrentPowerValue";
            this.labelCurrentPowerValue.Size = new System.Drawing.Size(97, 15);
            this.labelCurrentPowerValue.TabIndex = 13;
            this.labelCurrentPowerValue.Text = "Waiting for value";
            // 
            // groupBoxCurrentData
            // 
            this.groupBoxCurrentData.Controls.Add(this.powerGraph);
            this.groupBoxCurrentData.Controls.Add(this.distanceTraveledGraph);
            this.groupBoxCurrentData.Controls.Add(this.resistanceGraph);
            this.groupBoxCurrentData.Controls.Add(this.heartbeatGraph);
            this.groupBoxCurrentData.Controls.Add(this.speedGraph);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentSpeed);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentPowerValue);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentSpeedValue);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentDistanceValue);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentPower);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentResistanceValue);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentHearthbeat);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentHearthbeatValue);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentDistance);
            this.groupBoxCurrentData.Controls.Add(this.labelCurrentResistance);
            this.groupBoxCurrentData.Location = new System.Drawing.Point(20, 84);
            this.groupBoxCurrentData.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxCurrentData.Name = "groupBoxCurrentData";
            this.groupBoxCurrentData.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxCurrentData.Size = new System.Drawing.Size(598, 600);
            this.groupBoxCurrentData.TabIndex = 14;
            this.groupBoxCurrentData.TabStop = false;
            this.groupBoxCurrentData.Text = "Current Data";
            // 
            // powerGraph
            // 
            this.powerGraph.BackColor = System.Drawing.Color.White;
            this.powerGraph.DataSource = null;
            this.powerGraph.ForeColor = System.Drawing.Color.Red;
            this.powerGraph.GraphSizeMode = DoctorApplication.GraphControlSizeMode.Scroll;
            this.powerGraph.LineWidth = 1F;
            this.powerGraph.Location = new System.Drawing.Point(7, 479);
            this.powerGraph.MaxValue = null;
            this.powerGraph.MinValue = null;
            this.powerGraph.Name = "powerGraph";
            this.powerGraph.PointsToShow = 50;
            this.powerGraph.Size = new System.Drawing.Size(256, 108);
            this.powerGraph.TabIndex = 14;
            // 
            // distanceTraveledGraph
            // 
            this.distanceTraveledGraph.BackColor = System.Drawing.Color.White;
            this.distanceTraveledGraph.DataSource = null;
            this.distanceTraveledGraph.ForeColor = System.Drawing.Color.Red;
            this.distanceTraveledGraph.GraphSizeMode = DoctorApplication.GraphControlSizeMode.Stretch;
            this.distanceTraveledGraph.LineWidth = 1F;
            this.distanceTraveledGraph.Location = new System.Drawing.Point(7, 365);
            this.distanceTraveledGraph.MaxValue = null;
            this.distanceTraveledGraph.MinValue = null;
            this.distanceTraveledGraph.Name = "distanceTraveledGraph";
            this.distanceTraveledGraph.PointsToShow = 10;
            this.distanceTraveledGraph.Size = new System.Drawing.Size(256, 108);
            this.distanceTraveledGraph.TabIndex = 14;
            // 
            // resistanceGraph
            // 
            this.resistanceGraph.BackColor = System.Drawing.Color.White;
            this.resistanceGraph.DataSource = null;
            this.resistanceGraph.ForeColor = System.Drawing.Color.Red;
            this.resistanceGraph.GraphSizeMode = DoctorApplication.GraphControlSizeMode.Scroll;
            this.resistanceGraph.LineWidth = 1F;
            this.resistanceGraph.Location = new System.Drawing.Point(7, 251);
            this.resistanceGraph.MaxValue = null;
            this.resistanceGraph.MinValue = null;
            this.resistanceGraph.Name = "resistanceGraph";
            this.resistanceGraph.PointsToShow = 50;
            this.resistanceGraph.Size = new System.Drawing.Size(256, 108);
            this.resistanceGraph.TabIndex = 14;
            // 
            // heartbeatGraph
            // 
            this.heartbeatGraph.BackColor = System.Drawing.Color.White;
            this.heartbeatGraph.DataSource = null;
            this.heartbeatGraph.ForeColor = System.Drawing.Color.Red;
            this.heartbeatGraph.GraphSizeMode = DoctorApplication.GraphControlSizeMode.Scroll;
            this.heartbeatGraph.LineWidth = 1F;
            this.heartbeatGraph.Location = new System.Drawing.Point(7, 137);
            this.heartbeatGraph.MaxValue = null;
            this.heartbeatGraph.MinValue = null;
            this.heartbeatGraph.Name = "heartbeatGraph";
            this.heartbeatGraph.PointsToShow = 50;
            this.heartbeatGraph.Size = new System.Drawing.Size(256, 108);
            this.heartbeatGraph.TabIndex = 14;
            // 
            // speedGraph
            // 
            this.speedGraph.BackColor = System.Drawing.Color.White;
            this.speedGraph.DataSource = null;
            this.speedGraph.ForeColor = System.Drawing.Color.Red;
            this.speedGraph.GraphSizeMode = DoctorApplication.GraphControlSizeMode.Scroll;
            this.speedGraph.LineWidth = 1F;
            this.speedGraph.Location = new System.Drawing.Point(7, 23);
            this.speedGraph.MaxValue = null;
            this.speedGraph.MinValue = null;
            this.speedGraph.Name = "speedGraph";
            this.speedGraph.PointsToShow = 50;
            this.speedGraph.Size = new System.Drawing.Size(256, 108);
            this.speedGraph.TabIndex = 14;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Enabled = false;
            this.buttonStartStop.Location = new System.Drawing.Point(31, 748);
            this.buttonStartStop.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(122, 26);
            this.buttonStartStop.TabIndex = 15;
            this.buttonStartStop.Text = "Start/Stop training";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // EmergencyStopButton
            // 
            this.EmergencyStopButton.Location = new System.Drawing.Point(540, 748);
            this.EmergencyStopButton.Margin = new System.Windows.Forms.Padding(4);
            this.EmergencyStopButton.Name = "EmergencyStopButton";
            this.EmergencyStopButton.Size = new System.Drawing.Size(144, 26);
            this.EmergencyStopButton.TabIndex = 16;
            this.EmergencyStopButton.Text = "Emergency Shutdown";
            this.EmergencyStopButton.UseVisualStyleBackColor = true;
            this.EmergencyStopButton.Click += new System.EventHandler(this.EmergencyStopButton_Click);
            // 
            // labelRestistance
            // 
            this.labelRestistance.AutoSize = true;
            this.labelRestistance.Location = new System.Drawing.Point(27, 716);
            this.labelRestistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRestistance.Name = "labelRestistance";
            this.labelRestistance.Size = new System.Drawing.Size(86, 15);
            this.labelRestistance.TabIndex = 17;
            this.labelRestistance.Text = "Resistance(%): ";
            // 
            // textBoxResistance
            // 
            this.textBoxResistance.Enabled = false;
            this.textBoxResistance.Location = new System.Drawing.Point(127, 713);
            this.textBoxResistance.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxResistance.Name = "textBoxResistance";
            this.textBoxResistance.Size = new System.Drawing.Size(116, 23);
            this.textBoxResistance.TabIndex = 18;
            this.textBoxResistance.Text = "0.0";
            this.textBoxResistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxResistance_KeyPress);
            // 
            // trackBarResistance
            // 
            this.trackBarResistance.Enabled = false;
            this.trackBarResistance.Location = new System.Drawing.Point(262, 703);
            this.trackBarResistance.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarResistance.Maximum = 200;
            this.trackBarResistance.Name = "trackBarResistance";
            this.trackBarResistance.Size = new System.Drawing.Size(122, 45);
            this.trackBarResistance.TabIndex = 19;
            this.trackBarResistance.ValueChanged += new System.EventHandler(this.trackBarResistance_Changed);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(259, 66);
            this.labelTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(82, 15);
            this.labelTime.TabIndex = 20;
            this.labelTime.Text = "Training time: ";
            // 
            // labelTimeValue
            // 
            this.labelTimeValue.AutoSize = true;
            this.labelTimeValue.Location = new System.Drawing.Point(340, 66);
            this.labelTimeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimeValue.Name = "labelTimeValue";
            this.labelTimeValue.Size = new System.Drawing.Size(97, 15);
            this.labelTimeValue.TabIndex = 21;
            this.labelTimeValue.Text = "Waiting for value";
            this.labelTimeValue.UseWaitCursor = true;
            // 
            // LiveSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 786);
            this.Controls.Add(this.labelTimeValue);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.trackBarResistance);
            this.Controls.Add(this.textBoxResistance);
            this.Controls.Add(this.labelRestistance);
            this.Controls.Add(this.EmergencyStopButton);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.groupBoxCurrentData);
            this.Controls.Add(this.buttonSendChat);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.textBoxSendChat);
            this.Controls.Add(this.Patient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "LiveSession";
            this.Text = "Live Training Session";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LiveSession_FormClosing);
            this.Load += new System.EventHandler(this.LiveSession_Load);
            this.Shown += new System.EventHandler(this.LiveSession_Shown);
            this.groupBoxCurrentData.ResumeLayout(false);
            this.groupBoxCurrentData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Patient;
        private System.Windows.Forms.TextBox textBoxSendChat;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.Button buttonSendChat;
        private System.Windows.Forms.Label labelCurrentSpeed;
        private System.Windows.Forms.Label labelCurrentSpeedValue;
        private System.Windows.Forms.Label labelCurrentHearthbeat;
        private System.Windows.Forms.Label labelCurrentHearthbeatValue;
        private System.Windows.Forms.Label labelCurrentResistance;
        private System.Windows.Forms.Label labelCurrentResistanceValue;
        private System.Windows.Forms.Label labelCurrentDistance;
        private System.Windows.Forms.Label labelCurrentDistanceValue;
        private System.Windows.Forms.Label labelCurrentPower;
        private System.Windows.Forms.Label labelCurrentPowerValue;
        private System.Windows.Forms.GroupBox groupBoxCurrentData;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Button EmergencyStopButton;
        private System.Windows.Forms.Label labelRestistance;
        private System.Windows.Forms.TextBox textBoxResistance;
        private System.Windows.Forms.TrackBar trackBarResistance;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelTimeValue;
        private GraphControl speedGraph;
        private GraphControl powerGraph;
        private GraphControl distanceTraveledGraph;
        private GraphControl resistanceGraph;
        private GraphControl heartbeatGraph;
    }
}