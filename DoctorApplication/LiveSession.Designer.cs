namespace DoctorApplication
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
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.buttonNoodstop = new System.Windows.Forms.Button();
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
            this.Patient.Location = new System.Drawing.Point(16, 14);
            this.Patient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Patient.Name = "Patient";
            this.Patient.Size = new System.Drawing.Size(114, 31);
            this.Patient.TabIndex = 0;
            this.Patient.Text = "Patient: ";
            // 
            // textBoxSendChat
            // 
            this.textBoxSendChat.Location = new System.Drawing.Point(735, 872);
            this.textBoxSendChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSendChat.Name = "textBoxSendChat";
            this.textBoxSendChat.Size = new System.Drawing.Size(207, 27);
            this.textBoxSendChat.TabIndex = 1;
            // 
            // textBoxChat
            // 
            this.textBoxChat.Location = new System.Drawing.Point(735, 126);
            this.textBoxChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.ReadOnly = true;
            this.textBoxChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxChat.Size = new System.Drawing.Size(315, 730);
            this.textBoxChat.TabIndex = 2;
            // 
            // buttonSendChat
            // 
            this.buttonSendChat.Location = new System.Drawing.Point(951, 868);
            this.buttonSendChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSendChat.Name = "buttonSendChat";
            this.buttonSendChat.Size = new System.Drawing.Size(100, 35);
            this.buttonSendChat.TabIndex = 3;
            this.buttonSendChat.Text = "Send";
            this.buttonSendChat.UseVisualStyleBackColor = true;
            this.buttonSendChat.Click += new System.EventHandler(this.buttonSendChat_Click);
            // 
            // labelCurrentSpeed
            // 
            this.labelCurrentSpeed.AutoSize = true;
            this.labelCurrentSpeed.Location = new System.Drawing.Point(309, 68);
            this.labelCurrentSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentSpeed.Name = "labelCurrentSpeed";
            this.labelCurrentSpeed.Size = new System.Drawing.Size(110, 20);
            this.labelCurrentSpeed.TabIndex = 4;
            this.labelCurrentSpeed.Text = "Current Speed: ";
            // 
            // labelCurrentSpeedValue
            // 
            this.labelCurrentSpeedValue.AutoSize = true;
            this.labelCurrentSpeedValue.Location = new System.Drawing.Point(500, 68);
            this.labelCurrentSpeedValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentSpeedValue.Name = "labelCurrentSpeedValue";
            this.labelCurrentSpeedValue.Size = new System.Drawing.Size(122, 20);
            this.labelCurrentSpeedValue.TabIndex = 5;
            this.labelCurrentSpeedValue.Text = "Waiting for value";
            // 
            // labelCurrentHearthbeat
            // 
            this.labelCurrentHearthbeat.AutoSize = true;
            this.labelCurrentHearthbeat.Location = new System.Drawing.Point(309, 229);
            this.labelCurrentHearthbeat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentHearthbeat.Name = "labelCurrentHearthbeat";
            this.labelCurrentHearthbeat.Size = new System.Drawing.Size(135, 20);
            this.labelCurrentHearthbeat.TabIndex = 6;
            this.labelCurrentHearthbeat.Text = "Current Heartbeat: ";
            // 
            // labelCurrentHearthbeatValue
            // 
            this.labelCurrentHearthbeatValue.AutoSize = true;
            this.labelCurrentHearthbeatValue.Location = new System.Drawing.Point(500, 229);
            this.labelCurrentHearthbeatValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentHearthbeatValue.Name = "labelCurrentHearthbeatValue";
            this.labelCurrentHearthbeatValue.Size = new System.Drawing.Size(122, 20);
            this.labelCurrentHearthbeatValue.TabIndex = 7;
            this.labelCurrentHearthbeatValue.Text = "Waiting for value";
            // 
            // labelCurrentResistance
            // 
            this.labelCurrentResistance.AutoSize = true;
            this.labelCurrentResistance.Location = new System.Drawing.Point(309, 375);
            this.labelCurrentResistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentResistance.Name = "labelCurrentResistance";
            this.labelCurrentResistance.Size = new System.Drawing.Size(137, 20);
            this.labelCurrentResistance.TabIndex = 8;
            this.labelCurrentResistance.Text = "Current Resistance: ";
            // 
            // labelCurrentResistanceValue
            // 
            this.labelCurrentResistanceValue.AutoSize = true;
            this.labelCurrentResistanceValue.Location = new System.Drawing.Point(500, 375);
            this.labelCurrentResistanceValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentResistanceValue.Name = "labelCurrentResistanceValue";
            this.labelCurrentResistanceValue.Size = new System.Drawing.Size(122, 20);
            this.labelCurrentResistanceValue.TabIndex = 9;
            this.labelCurrentResistanceValue.Text = "Waiting for value";
            // 
            // labelCurrentDistance
            // 
            this.labelCurrentDistance.AutoSize = true;
            this.labelCurrentDistance.Location = new System.Drawing.Point(309, 534);
            this.labelCurrentDistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentDistance.Name = "labelCurrentDistance";
            this.labelCurrentDistance.Size = new System.Drawing.Size(185, 20);
            this.labelCurrentDistance.TabIndex = 10;
            this.labelCurrentDistance.Text = "Current Distance Traveled: ";
            this.labelCurrentDistance.Click += new System.EventHandler(this.labelCurrentDistance_Click);
            // 
            // labelCurrentDistanceValue
            // 
            this.labelCurrentDistanceValue.AutoSize = true;
            this.labelCurrentDistanceValue.Location = new System.Drawing.Point(500, 534);
            this.labelCurrentDistanceValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentDistanceValue.Name = "labelCurrentDistanceValue";
            this.labelCurrentDistanceValue.Size = new System.Drawing.Size(122, 20);
            this.labelCurrentDistanceValue.TabIndex = 11;
            this.labelCurrentDistanceValue.Text = "Waiting for value";
            // 
            // labelCurrentPower
            // 
            this.labelCurrentPower.AutoSize = true;
            this.labelCurrentPower.Location = new System.Drawing.Point(309, 686);
            this.labelCurrentPower.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentPower.Name = "labelCurrentPower";
            this.labelCurrentPower.Size = new System.Drawing.Size(108, 20);
            this.labelCurrentPower.TabIndex = 12;
            this.labelCurrentPower.Text = "Current Power: ";
            this.labelCurrentPower.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelCurrentPowerValue
            // 
            this.labelCurrentPowerValue.AutoSize = true;
            this.labelCurrentPowerValue.Location = new System.Drawing.Point(500, 686);
            this.labelCurrentPowerValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCurrentPowerValue.Name = "labelCurrentPowerValue";
            this.labelCurrentPowerValue.Size = new System.Drawing.Size(122, 20);
            this.labelCurrentPowerValue.TabIndex = 13;
            this.labelCurrentPowerValue.Text = "Waiting for value";
            // 
            // groupBoxCurrentData
            // 
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
            this.groupBoxCurrentData.Location = new System.Drawing.Point(23, 112);
            this.groupBoxCurrentData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxCurrentData.Name = "groupBoxCurrentData";
            this.groupBoxCurrentData.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxCurrentData.Size = new System.Drawing.Size(683, 800);
            this.groupBoxCurrentData.TabIndex = 14;
            this.groupBoxCurrentData.TabStop = false;
            this.groupBoxCurrentData.Text = "Current Data";
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(35, 997);
            this.buttonStartStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(139, 35);
            this.buttonStartStop.TabIndex = 15;
            this.buttonStartStop.Text = "Start/Stop training";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // buttonNoodstop
            // 
            this.buttonNoodstop.Location = new System.Drawing.Point(617, 997);
            this.buttonNoodstop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonNoodstop.Name = "buttonNoodstop";
            this.buttonNoodstop.Size = new System.Drawing.Size(100, 35);
            this.buttonNoodstop.TabIndex = 16;
            this.buttonNoodstop.Text = "Noodstop";
            this.buttonNoodstop.UseVisualStyleBackColor = true;
            // 
            // labelRestistance
            // 
            this.labelRestistance.AutoSize = true;
            this.labelRestistance.Location = new System.Drawing.Point(31, 955);
            this.labelRestistance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRestistance.Name = "labelRestistance";
            this.labelRestistance.Size = new System.Drawing.Size(107, 20);
            this.labelRestistance.TabIndex = 17;
            this.labelRestistance.Text = "Resistance(%): ";
            // 
            // textBoxResistance
            // 
            this.textBoxResistance.Location = new System.Drawing.Point(145, 951);
            this.textBoxResistance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxResistance.Name = "textBoxResistance";
            this.textBoxResistance.Size = new System.Drawing.Size(132, 27);
            this.textBoxResistance.TabIndex = 18;
            // 
            // trackBarResistance
            // 
            this.trackBarResistance.Location = new System.Drawing.Point(300, 937);
            this.trackBarResistance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.trackBarResistance.Name = "trackBarResistance";
            this.trackBarResistance.Size = new System.Drawing.Size(139, 56);
            this.trackBarResistance.TabIndex = 19;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(296, 88);
            this.labelTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(103, 20);
            this.labelTime.TabIndex = 20;
            this.labelTime.Text = "Training time: ";
            // 
            // labelTimeValue
            // 
            this.labelTimeValue.AutoSize = true;
            this.labelTimeValue.Location = new System.Drawing.Point(388, 88);
            this.labelTimeValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimeValue.Name = "labelTimeValue";
            this.labelTimeValue.Size = new System.Drawing.Size(122, 20);
            this.labelTimeValue.TabIndex = 21;
            this.labelTimeValue.Text = "Waiting for value";
            this.labelTimeValue.UseWaitCursor = true;
            // 
            // LiveSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 1048);
            this.Controls.Add(this.labelTimeValue);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.trackBarResistance);
            this.Controls.Add(this.textBoxResistance);
            this.Controls.Add(this.labelRestistance);
            this.Controls.Add(this.buttonNoodstop);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.groupBoxCurrentData);
            this.Controls.Add(this.buttonSendChat);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.textBoxSendChat);
            this.Controls.Add(this.Patient);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LiveSession";
            this.Text = "LiveSession";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LiveSession_FormClosing);
            this.Load += new System.EventHandler(this.LiveSession_Load);
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
        private System.Windows.Forms.Button buttonNoodstop;
        private System.Windows.Forms.Label labelRestistance;
        private System.Windows.Forms.TextBox textBoxResistance;
        private System.Windows.Forms.TrackBar trackBarResistance;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelTimeValue;
    }
}