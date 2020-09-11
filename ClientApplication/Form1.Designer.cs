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
            this.groupBoxSimulator = new System.Windows.Forms.GroupBox();
            this.groupBoxBikeSettings = new System.Windows.Forms.GroupBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxBikeName = new System.Windows.Forms.TextBox();
            this.radioButtonSimulator = new System.Windows.Forms.RadioButton();
            this.radioButtonBike = new System.Windows.Forms.RadioButton();
            this.labelUseText = new System.Windows.Forms.Label();
            this.labelBikeNameText = new System.Windows.Forms.Label();
            this.groupBoxBikeData = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeartbeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).BeginInit();
            this.groupBoxSimulator.SuspendLayout();
            this.groupBoxBikeSettings.SuspendLayout();
            this.groupBoxBikeData.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(102, 23);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(147, 27);
            this.textBoxSpeed.TabIndex = 0;
            this.textBoxSpeed.Text = "0";
            this.textBoxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSpeed_KeyPress);
            // 
            // textBoxHeartbeat
            // 
            this.textBoxHeartbeat.Location = new System.Drawing.Point(102, 88);
            this.textBoxHeartbeat.Name = "textBoxHeartbeat";
            this.textBoxHeartbeat.Size = new System.Drawing.Size(147, 27);
            this.textBoxHeartbeat.TabIndex = 1;
            this.textBoxHeartbeat.Text = "0";
            this.textBoxHeartbeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHeartbeat_KeyPress);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(6, 27);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(90, 20);
            this.labelSpeed.TabIndex = 3;
            this.labelSpeed.Text = "Speed (m/s)";
            // 
            // labelHeartbeat
            // 
            this.labelHeartbeat.AutoSize = true;
            this.labelHeartbeat.Location = new System.Drawing.Point(7, 92);
            this.labelHeartbeat.Name = "labelHeartbeat";
            this.labelHeartbeat.Size = new System.Drawing.Size(76, 20);
            this.labelHeartbeat.TabIndex = 4;
            this.labelHeartbeat.Text = "HeartBeat";
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(267, 23);
            this.trackBarSpeed.Maximum = 4000;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(147, 56);
            this.trackBarSpeed.TabIndex = 7;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // trackBarHeartbeat
            // 
            this.trackBarHeartbeat.Location = new System.Drawing.Point(267, 88);
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
            this.labelCurrentSpeedText.Location = new System.Drawing.Point(7, 25);
            this.labelCurrentSpeedText.Name = "labelCurrentSpeedText";
            this.labelCurrentSpeedText.Size = new System.Drawing.Size(110, 20);
            this.labelCurrentSpeedText.TabIndex = 8;
            this.labelCurrentSpeedText.Text = "Current Speed: ";
            // 
            // labelCurrentSpeedValue
            // 
            this.labelCurrentSpeedValue.AutoSize = true;
            this.labelCurrentSpeedValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentSpeedValue.Location = new System.Drawing.Point(194, 27);
            this.labelCurrentSpeedValue.Name = "labelCurrentSpeedValue";
            this.labelCurrentSpeedValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentSpeedValue.TabIndex = 9;
            this.labelCurrentSpeedValue.Text = "waiting for value";
            // 
            // labelCurrentHeartbeatText
            // 
            this.labelCurrentHeartbeatText.AutoSize = true;
            this.labelCurrentHeartbeatText.Location = new System.Drawing.Point(7, 56);
            this.labelCurrentHeartbeatText.Name = "labelCurrentHeartbeatText";
            this.labelCurrentHeartbeatText.Size = new System.Drawing.Size(135, 20);
            this.labelCurrentHeartbeatText.TabIndex = 10;
            this.labelCurrentHeartbeatText.Text = "Current Heartbeat: ";
            // 
            // labelCurrentHeartbeatValue
            // 
            this.labelCurrentHeartbeatValue.AutoSize = true;
            this.labelCurrentHeartbeatValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentHeartbeatValue.Location = new System.Drawing.Point(194, 56);
            this.labelCurrentHeartbeatValue.Name = "labelCurrentHeartbeatValue";
            this.labelCurrentHeartbeatValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentHeartbeatValue.TabIndex = 11;
            this.labelCurrentHeartbeatValue.Text = "waiting for value";
            // 
            // labelCurrentResistanceText
            // 
            this.labelCurrentResistanceText.AutoSize = true;
            this.labelCurrentResistanceText.Location = new System.Drawing.Point(7, 87);
            this.labelCurrentResistanceText.Name = "labelCurrentResistanceText";
            this.labelCurrentResistanceText.Size = new System.Drawing.Size(137, 20);
            this.labelCurrentResistanceText.TabIndex = 12;
            this.labelCurrentResistanceText.Text = "Current Resistance: ";
            // 
            // labelCurrentResistanceValue
            // 
            this.labelCurrentResistanceValue.AutoSize = true;
            this.labelCurrentResistanceValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentResistanceValue.Location = new System.Drawing.Point(194, 87);
            this.labelCurrentResistanceValue.Name = "labelCurrentResistanceValue";
            this.labelCurrentResistanceValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentResistanceValue.TabIndex = 13;
            this.labelCurrentResistanceValue.Text = "waiting for value";
            // 
            // labelResistance
            // 
            this.labelResistance.AutoSize = true;
            this.labelResistance.Location = new System.Drawing.Point(7, 119);
            this.labelResistance.Name = "labelResistance";
            this.labelResistance.Size = new System.Drawing.Size(78, 20);
            this.labelResistance.TabIndex = 14;
            this.labelResistance.Text = "Resistance";
            // 
            // textBoxResistance
            // 
            this.textBoxResistance.Location = new System.Drawing.Point(102, 115);
            this.textBoxResistance.Name = "textBoxResistance";
            this.textBoxResistance.Size = new System.Drawing.Size(147, 27);
            this.textBoxResistance.TabIndex = 15;
            this.textBoxResistance.Text = "0";
            this.textBoxResistance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxResistance_KeyPress);
            // 
            // trackBarResistance
            // 
            this.trackBarResistance.Location = new System.Drawing.Point(267, 115);
            this.trackBarResistance.Maximum = 200;
            this.trackBarResistance.Name = "trackBarResistance";
            this.trackBarResistance.Size = new System.Drawing.Size(147, 56);
            this.trackBarResistance.TabIndex = 16;
            this.trackBarResistance.Value = 1;
            this.trackBarResistance.Scroll += new System.EventHandler(this.trackBarResistance_Scroll);
            // 
            // labelCurrentElapsedTimeText
            // 
            this.labelCurrentElapsedTimeText.AutoSize = true;
            this.labelCurrentElapsedTimeText.Location = new System.Drawing.Point(7, 119);
            this.labelCurrentElapsedTimeText.Name = "labelCurrentElapsedTimeText";
            this.labelCurrentElapsedTimeText.Size = new System.Drawing.Size(157, 20);
            this.labelCurrentElapsedTimeText.TabIndex = 17;
            this.labelCurrentElapsedTimeText.Text = "Current Elapsed Time: ";
            // 
            // labelCurrentElapsedTimeValue
            // 
            this.labelCurrentElapsedTimeValue.AutoSize = true;
            this.labelCurrentElapsedTimeValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentElapsedTimeValue.Location = new System.Drawing.Point(194, 119);
            this.labelCurrentElapsedTimeValue.Name = "labelCurrentElapsedTimeValue";
            this.labelCurrentElapsedTimeValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentElapsedTimeValue.TabIndex = 18;
            this.labelCurrentElapsedTimeValue.Text = "waiting for value";
            // 
            // labelDistanceTraveledText
            // 
            this.labelDistanceTraveledText.AutoSize = true;
            this.labelDistanceTraveledText.Location = new System.Drawing.Point(7, 149);
            this.labelDistanceTraveledText.Name = "labelDistanceTraveledText";
            this.labelDistanceTraveledText.Size = new System.Drawing.Size(185, 20);
            this.labelDistanceTraveledText.TabIndex = 19;
            this.labelDistanceTraveledText.Text = "Current Distance Traveled: ";
            // 
            // labelCurrentDistanceTraveledValue
            // 
            this.labelCurrentDistanceTraveledValue.AutoSize = true;
            this.labelCurrentDistanceTraveledValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentDistanceTraveledValue.Location = new System.Drawing.Point(194, 149);
            this.labelCurrentDistanceTraveledValue.Name = "labelCurrentDistanceTraveledValue";
            this.labelCurrentDistanceTraveledValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentDistanceTraveledValue.TabIndex = 20;
            this.labelCurrentDistanceTraveledValue.Text = "waiting for value";
            // 
            // labelCurrentPowerText
            // 
            this.labelCurrentPowerText.AutoSize = true;
            this.labelCurrentPowerText.Location = new System.Drawing.Point(7, 180);
            this.labelCurrentPowerText.Name = "labelCurrentPowerText";
            this.labelCurrentPowerText.Size = new System.Drawing.Size(108, 20);
            this.labelCurrentPowerText.TabIndex = 21;
            this.labelCurrentPowerText.Text = "Current Power: ";
            // 
            // labelCurrentPowerValue
            // 
            this.labelCurrentPowerValue.AutoSize = true;
            this.labelCurrentPowerValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentPowerValue.Location = new System.Drawing.Point(194, 180);
            this.labelCurrentPowerValue.Name = "labelCurrentPowerValue";
            this.labelCurrentPowerValue.Size = new System.Drawing.Size(127, 20);
            this.labelCurrentPowerValue.TabIndex = 22;
            this.labelCurrentPowerValue.Text = "waiting for value";
            // 
            // groupBoxSimulator
            // 
            this.groupBoxSimulator.Controls.Add(this.trackBarHeartbeat);
            this.groupBoxSimulator.Controls.Add(this.textBoxHeartbeat);
            this.groupBoxSimulator.Controls.Add(this.textBoxSpeed);
            this.groupBoxSimulator.Controls.Add(this.labelSpeed);
            this.groupBoxSimulator.Controls.Add(this.labelHeartbeat);
            this.groupBoxSimulator.Controls.Add(this.trackBarSpeed);
            this.groupBoxSimulator.Location = new System.Drawing.Point(14, 16);
            this.groupBoxSimulator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxSimulator.Name = "groupBoxSimulator";
            this.groupBoxSimulator.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxSimulator.Size = new System.Drawing.Size(422, 152);
            this.groupBoxSimulator.TabIndex = 23;
            this.groupBoxSimulator.TabStop = false;
            this.groupBoxSimulator.Text = "Simulator";
            // 
            // groupBoxBikeSettings
            // 
            this.groupBoxBikeSettings.Controls.Add(this.buttonConnect);
            this.groupBoxBikeSettings.Controls.Add(this.textBoxBikeName);
            this.groupBoxBikeSettings.Controls.Add(this.radioButtonSimulator);
            this.groupBoxBikeSettings.Controls.Add(this.radioButtonBike);
            this.groupBoxBikeSettings.Controls.Add(this.labelUseText);
            this.groupBoxBikeSettings.Controls.Add(this.textBoxResistance);
            this.groupBoxBikeSettings.Controls.Add(this.labelResistance);
            this.groupBoxBikeSettings.Controls.Add(this.trackBarResistance);
            this.groupBoxBikeSettings.Location = new System.Drawing.Point(14, 188);
            this.groupBoxBikeSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxBikeSettings.Name = "groupBoxBikeSettings";
            this.groupBoxBikeSettings.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxBikeSettings.Size = new System.Drawing.Size(422, 181);
            this.groupBoxBikeSettings.TabIndex = 24;
            this.groupBoxBikeSettings.TabStop = false;
            this.groupBoxBikeSettings.Text = "Bike Settings";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(267, 68);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(147, 31);
            this.buttonConnect.TabIndex = 29;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxBikeName
            // 
            this.textBoxBikeName.Location = new System.Drawing.Point(102, 69);
            this.textBoxBikeName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBikeName.Name = "textBoxBikeName";
            this.textBoxBikeName.Size = new System.Drawing.Size(147, 27);
            this.textBoxBikeName.TabIndex = 28;
            this.textBoxBikeName.Text = "Avans Bike";
            // 
            // radioButtonSimulator
            // 
            this.radioButtonSimulator.AutoSize = true;
            this.radioButtonSimulator.Location = new System.Drawing.Point(162, 32);
            this.radioButtonSimulator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonSimulator.Name = "radioButtonSimulator";
            this.radioButtonSimulator.Size = new System.Drawing.Size(94, 24);
            this.radioButtonSimulator.TabIndex = 27;
            this.radioButtonSimulator.Text = "Simulator";
            this.radioButtonSimulator.UseVisualStyleBackColor = true;
            this.radioButtonSimulator.CheckedChanged += new System.EventHandler(this.radioButtonSimulator_CheckedChanged);
            // 
            // radioButtonBike
            // 
            this.radioButtonBike.AutoSize = true;
            this.radioButtonBike.Checked = true;
            this.radioButtonBike.Location = new System.Drawing.Point(102, 32);
            this.radioButtonBike.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonBike.Name = "radioButtonBike";
            this.radioButtonBike.Size = new System.Drawing.Size(58, 24);
            this.radioButtonBike.TabIndex = 26;
            this.radioButtonBike.TabStop = true;
            this.radioButtonBike.Text = "Bike";
            this.radioButtonBike.UseVisualStyleBackColor = true;
            this.radioButtonBike.CheckedChanged += new System.EventHandler(this.radioButtonBike_CheckedChanged);
            // 
            // labelUseText
            // 
            this.labelUseText.AutoSize = true;
            this.labelUseText.Location = new System.Drawing.Point(7, 35);
            this.labelUseText.Name = "labelUseText";
            this.labelUseText.Size = new System.Drawing.Size(33, 20);
            this.labelUseText.TabIndex = 25;
            this.labelUseText.Text = "Use";
            // 
            // labelBikeNameText
            // 
            this.labelBikeNameText.AutoSize = true;
            this.labelBikeNameText.Location = new System.Drawing.Point(21, 261);
            this.labelBikeNameText.Name = "labelBikeNameText";
            this.labelBikeNameText.Size = new System.Drawing.Size(81, 20);
            this.labelBikeNameText.TabIndex = 28;
            this.labelBikeNameText.Text = "Bike Name";
            // 
            // groupBoxBikeData
            // 
            this.groupBoxBikeData.Controls.Add(this.labelCurrentSpeedText);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentSpeedValue);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentHeartbeatText);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentHeartbeatValue);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentPowerValue);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentResistanceText);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentPowerText);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentResistanceValue);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentDistanceTraveledValue);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentElapsedTimeText);
            this.groupBoxBikeData.Controls.Add(this.labelDistanceTraveledText);
            this.groupBoxBikeData.Controls.Add(this.labelCurrentElapsedTimeValue);
            this.groupBoxBikeData.Location = new System.Drawing.Point(442, 16);
            this.groupBoxBikeData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxBikeData.Name = "groupBoxBikeData";
            this.groupBoxBikeData.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxBikeData.Size = new System.Drawing.Size(344, 353);
            this.groupBoxBikeData.TabIndex = 29;
            this.groupBoxBikeData.TabStop = false;
            this.groupBoxBikeData.Text = "Bike Data";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 380);
            this.Controls.Add(this.groupBoxBikeData);
            this.Controls.Add(this.labelBikeNameText);
            this.Controls.Add(this.groupBoxBikeSettings);
            this.Controls.Add(this.groupBoxSimulator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeartbeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).EndInit();
            this.groupBoxSimulator.ResumeLayout(false);
            this.groupBoxSimulator.PerformLayout();
            this.groupBoxBikeSettings.ResumeLayout(false);
            this.groupBoxBikeSettings.PerformLayout();
            this.groupBoxBikeData.ResumeLayout(false);
            this.groupBoxBikeData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.TextBox textBoxHeartbeat;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelHeartbeat;
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
        private System.Windows.Forms.GroupBox groupBoxSimulator;
        private System.Windows.Forms.GroupBox groupBoxBikeSettings;
        private System.Windows.Forms.RadioButton radioButtonSimulator;
        private System.Windows.Forms.RadioButton radioButtonBike;
        private System.Windows.Forms.Label labelUseText;
        private System.Windows.Forms.TextBox textBoxBikeName;
        private System.Windows.Forms.Label labelBikeNameText;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.GroupBox groupBoxBikeData;
    }
}

