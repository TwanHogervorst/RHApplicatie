namespace ClientApplication
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.textBoxHeartbeat = new System.Windows.Forms.TextBox();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelHeartbeat = new System.Windows.Forms.Label();
            this.buttonSendData = new System.Windows.Forms.Button();
            this.trackBarSpeed = new System.Windows.Forms.TrackBar();
            this.trackBarHeartbeat = new System.Windows.Forms.TrackBar();
            this.labelCurrentSpeedText = new System.Windows.Forms.Label();
            this.labelCurrentSpeedValue = new System.Windows.Forms.Label();
            this.labelCurrentHeartbeatText = new System.Windows.Forms.Label();
            this.labelCurrentHeartbeatValue = new System.Windows.Forms.Label();
            this.labelCurrentResistanceText = new System.Windows.Forms.Label();
            this.labelCurrentResistanceValue = new System.Windows.Forms.Label();
            this.labelResistance = new System.Windows.Forms.Label();
            this.textBoxResistance = new System.Windows.Forms.TextBox();
            this.trackBarResistance = new System.Windows.Forms.TrackBar();
            this.labelCurrentElapsedTimeText = new System.Windows.Forms.Label();
            this.labelCurrentElapsedTimeValue = new System.Windows.Forms.Label();
            this.labelDistanceTraveledText = new System.Windows.Forms.Label();
            this.labelCurrentDistanceTraveledValue = new System.Windows.Forms.Label();
            this.labelCurrentPowerText = new System.Windows.Forms.Label();
            this.labelCurrentPowerValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeartbeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(108, 6);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(147, 27);
            this.textBoxSpeed.TabIndex = 0;
            this.textBoxSpeed.Text = "0";
            this.textBoxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSpeed_KeyPress);
            // 
            // textBoxHeartbeat
            // 
            this.textBoxHeartbeat.Location = new System.Drawing.Point(108, 39);
            this.textBoxHeartbeat.Name = "textBoxHeartbeat";
            this.textBoxHeartbeat.Size = new System.Drawing.Size(147, 27);
            this.textBoxHeartbeat.TabIndex = 1;
            this.textBoxHeartbeat.Text = "0";
            this.textBoxHeartbeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHeartbeat_KeyPress);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(12, 9);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(90, 20);
            this.labelSpeed.TabIndex = 3;
            this.labelSpeed.Text = "Speed (m/s)";
            // 
            // labelHeartbeat
            // 
            this.labelHeartbeat.AutoSize = true;
            this.labelHeartbeat.Location = new System.Drawing.Point(12, 42);
            this.labelHeartbeat.Name = "labelHeartbeat";
            this.labelHeartbeat.Size = new System.Drawing.Size(39, 20);
            this.labelHeartbeat.TabIndex = 4;
            this.labelHeartbeat.Text = "BPM";
            // 
            // buttonSendData
            // 
            this.buttonSendData.Location = new System.Drawing.Point(362, 389);
            this.buttonSendData.Name = "buttonSendData";
            this.buttonSendData.Size = new System.Drawing.Size(94, 29);
            this.buttonSendData.TabIndex = 6;
            this.buttonSendData.Text = "Send Data";
            this.buttonSendData.UseVisualStyleBackColor = true;
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(261, 6);
            this.trackBarSpeed.Maximum = 4000;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(147, 56);
            this.trackBarSpeed.TabIndex = 7;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // trackBarHeartbeat
            // 
            this.trackBarHeartbeat.Location = new System.Drawing.Point(261, 42);
            this.trackBarHeartbeat.Maximum = 230;
            this.trackBarHeartbeat.Minimum = 50;
            this.trackBarHeartbeat.Name = "trackBarHeartbeat";
            this.trackBarHeartbeat.Size = new System.Drawing.Size(147, 56);
            this.trackBarHeartbeat.TabIndex = 7;
            this.trackBarHeartbeat.Value = 50;
            this.trackBarHeartbeat.Scroll += new System.EventHandler(this.trackBarHeartbeat_Scroll);
            // 
            // labelCurrentSpeedText
            // 
            this.labelCurrentSpeedText.AutoSize = true;
            this.labelCurrentSpeedText.Location = new System.Drawing.Point(473, 8);
            this.labelCurrentSpeedText.Name = "labelCurrentSpeedText";
            this.labelCurrentSpeedText.Size = new System.Drawing.Size(110, 20);
            this.labelCurrentSpeedText.TabIndex = 8;
            this.labelCurrentSpeedText.Text = "Current Speed: ";
            // 
            // labelCurrentSpeedValue
            // 
            this.labelCurrentSpeedValue.AutoSize = true;
            this.labelCurrentSpeedValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentSpeedValue.Location = new System.Drawing.Point(661, 9);
            this.labelCurrentSpeedValue.Name = "labelCurrentSpeedValue";
            this.labelCurrentSpeedValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentSpeedValue.TabIndex = 9;
            this.labelCurrentSpeedValue.Text = "waiting for value";
            // 
            // labelCurrentHeartbeatText
            // 
            this.labelCurrentHeartbeatText.AutoSize = true;
            this.labelCurrentHeartbeatText.Location = new System.Drawing.Point(473, 39);
            this.labelCurrentHeartbeatText.Name = "labelCurrentHeartbeatText";
            this.labelCurrentHeartbeatText.Size = new System.Drawing.Size(135, 20);
            this.labelCurrentHeartbeatText.TabIndex = 10;
            this.labelCurrentHeartbeatText.Text = "Current Heartbeat: ";
            // 
            // labelCurrentHeartbeatValue
            // 
            this.labelCurrentHeartbeatValue.AutoSize = true;
            this.labelCurrentHeartbeatValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentHeartbeatValue.Location = new System.Drawing.Point(661, 39);
            this.labelCurrentHeartbeatValue.Name = "labelCurrentHeartbeatValue";
            this.labelCurrentHeartbeatValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentHeartbeatValue.TabIndex = 11;
            this.labelCurrentHeartbeatValue.Text = "waiting for value";
            // 
            // labelCurrentResistanceText
            // 
            this.labelCurrentResistanceText.AutoSize = true;
            this.labelCurrentResistanceText.Location = new System.Drawing.Point(473, 70);
            this.labelCurrentResistanceText.Name = "labelCurrentResistanceText";
            this.labelCurrentResistanceText.Size = new System.Drawing.Size(137, 20);
            this.labelCurrentResistanceText.TabIndex = 12;
            this.labelCurrentResistanceText.Text = "Current Resistance: ";
            // 
            // labelCurrentResistanceValue
            // 
            this.labelCurrentResistanceValue.AutoSize = true;
            this.labelCurrentResistanceValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentResistanceValue.Location = new System.Drawing.Point(661, 70);
            this.labelCurrentResistanceValue.Name = "labelCurrentResistanceValue";
            this.labelCurrentResistanceValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentResistanceValue.TabIndex = 13;
            this.labelCurrentResistanceValue.Text = "waiting for value";
            // 
            // labelResistance
            // 
            this.labelResistance.AutoSize = true;
            this.labelResistance.Location = new System.Drawing.Point(12, 78);
            this.labelResistance.Name = "labelResistance";
            this.labelResistance.Size = new System.Drawing.Size(78, 20);
            this.labelResistance.TabIndex = 14;
            this.labelResistance.Text = "Resistance";
            // 
            // textBoxResistance
            // 
            this.textBoxResistance.Location = new System.Drawing.Point(108, 78);
            this.textBoxResistance.Name = "textBoxResistance";
            this.textBoxResistance.Size = new System.Drawing.Size(147, 27);
            this.textBoxResistance.TabIndex = 15;
            this.textBoxResistance.Text = "1";
            this.textBoxResistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxResistance_KeyPress);
            // 
            // trackBarResistance
            // 
            this.trackBarResistance.Location = new System.Drawing.Point(261, 78);
            this.trackBarResistance.Maximum = 200;
            this.trackBarResistance.Minimum = 1;
            this.trackBarResistance.Name = "trackBarResistance";
            this.trackBarResistance.Size = new System.Drawing.Size(147, 56);
            this.trackBarResistance.TabIndex = 16;
            this.trackBarResistance.Value = 1;
            this.trackBarResistance.Scroll += new System.EventHandler(this.trackBarResistance_Scroll);
            // 
            // labelCurrentElapsedTimeText
            // 
            this.labelCurrentElapsedTimeText.AutoSize = true;
            this.labelCurrentElapsedTimeText.Location = new System.Drawing.Point(473, 101);
            this.labelCurrentElapsedTimeText.Name = "labelCurrentElapsedTimeText";
            this.labelCurrentElapsedTimeText.Size = new System.Drawing.Size(157, 20);
            this.labelCurrentElapsedTimeText.TabIndex = 17;
            this.labelCurrentElapsedTimeText.Text = "Current Elapsed Time: ";
            // 
            // labelCurrentElapsedTimeValue
            // 
            this.labelCurrentElapsedTimeValue.AutoSize = true;
            this.labelCurrentElapsedTimeValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentElapsedTimeValue.Location = new System.Drawing.Point(661, 101);
            this.labelCurrentElapsedTimeValue.Name = "labelCurrentElapsedTimeValue";
            this.labelCurrentElapsedTimeValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentElapsedTimeValue.TabIndex = 18;
            this.labelCurrentElapsedTimeValue.Text = "waiting for value";
            // 
            // labelDistanceTraveledText
            // 
            this.labelDistanceTraveledText.AutoSize = true;
            this.labelDistanceTraveledText.Location = new System.Drawing.Point(473, 132);
            this.labelDistanceTraveledText.Name = "labelDistanceTraveledText";
            this.labelDistanceTraveledText.Size = new System.Drawing.Size(185, 20);
            this.labelDistanceTraveledText.TabIndex = 19;
            this.labelDistanceTraveledText.Text = "Current Distance Traveled: ";
            // 
            // labelCurrentDistanceTraveledValue
            // 
            this.labelCurrentDistanceTraveledValue.AutoSize = true;
            this.labelCurrentDistanceTraveledValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentDistanceTraveledValue.Location = new System.Drawing.Point(661, 132);
            this.labelCurrentDistanceTraveledValue.Name = "labelCurrentDistanceTraveledValue";
            this.labelCurrentDistanceTraveledValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentDistanceTraveledValue.TabIndex = 20;
            this.labelCurrentDistanceTraveledValue.Text = "waiting for value";
            // 
            // labelCurrentPowerText
            // 
            this.labelCurrentPowerText.AutoSize = true;
            this.labelCurrentPowerText.Location = new System.Drawing.Point(473, 163);
            this.labelCurrentPowerText.Name = "labelCurrentPowerText";
            this.labelCurrentPowerText.Size = new System.Drawing.Size(108, 20);
            this.labelCurrentPowerText.TabIndex = 21;
            this.labelCurrentPowerText.Text = "Current Power: ";
            // 
            // labelCurrentPowerValue
            // 
            this.labelCurrentPowerValue.AutoSize = true;
            this.labelCurrentPowerValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentPowerValue.Location = new System.Drawing.Point(661, 163);
            this.labelCurrentPowerValue.Name = "labelCurrentPowerValue";
            this.labelCurrentPowerValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentPowerValue.TabIndex = 22;
            this.labelCurrentPowerValue.Text = "waiting for value";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelCurrentPowerValue);
            this.Controls.Add(this.labelCurrentPowerText);
            this.Controls.Add(this.labelCurrentDistanceTraveledValue);
            this.Controls.Add(this.labelDistanceTraveledText);
            this.Controls.Add(this.labelCurrentElapsedTimeValue);
            this.Controls.Add(this.labelCurrentElapsedTimeText);
            this.Controls.Add(this.trackBarResistance);
            this.Controls.Add(this.textBoxResistance);
            this.Controls.Add(this.labelResistance);
            this.Controls.Add(this.labelCurrentResistanceValue);
            this.Controls.Add(this.labelCurrentResistanceText);
            this.Controls.Add(this.labelCurrentHeartbeatValue);
            this.Controls.Add(this.labelCurrentHeartbeatText);
            this.Controls.Add(this.labelCurrentSpeedValue);
            this.Controls.Add(this.labelCurrentSpeedText);
            this.Controls.Add(this.trackBarHeartbeat);
            this.Controls.Add(this.trackBarSpeed);
            this.Controls.Add(this.buttonSendData);
            this.Controls.Add(this.labelHeartbeat);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.textBoxHeartbeat);
            this.Controls.Add(this.textBoxSpeed);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeartbeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.TextBox textBoxHeartbeat;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelHeartbeat;
        private System.Windows.Forms.Button buttonSendData;
        private System.Windows.Forms.TrackBar trackBarSpeed;
        private System.Windows.Forms.TrackBar trackBarHeartbeat;
        private System.Windows.Forms.Label labelCurrentSpeedText;
        private System.Windows.Forms.Label labelCurrentSpeedValue;
        private System.Windows.Forms.Label labelCurrentHeartbeatText;
        private System.Windows.Forms.Label labelCurrentHeartbeatValue;
        private System.Windows.Forms.Label labelCurrentResistanceText;
        private System.Windows.Forms.Label labelCurrentResistanceValue;
        private System.Windows.Forms.Label labelResistance;
        private System.Windows.Forms.TextBox textBoxResistance;
        private System.Windows.Forms.TrackBar trackBarResistance;
        private System.Windows.Forms.Label labelCurrentElapsedTimeText;
        private System.Windows.Forms.Label labelCurrentElapsedTimeValue;
        private System.Windows.Forms.Label labelDistanceTraveledText;
        private System.Windows.Forms.Label labelCurrentDistanceTraveledValue;
        private System.Windows.Forms.Label labelCurrentPowerText;
        private System.Windows.Forms.Label labelCurrentPowerValue;
    }
}

