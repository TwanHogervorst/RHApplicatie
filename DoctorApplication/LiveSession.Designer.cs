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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.speedGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.heartbeatGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.resistanceGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.distanceGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.powerGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxCurrentData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heartbeatGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resistanceGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // Patient
            // 
            this.Patient.AutoSize = true;
            this.Patient.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Patient.Location = new System.Drawing.Point(12, 9);
            this.Patient.Name = "Patient";
            this.Patient.Size = new System.Drawing.Size(91, 25);
            this.Patient.TabIndex = 0;
            this.Patient.Text = "Patient: ";
            // 
            // textBoxSendChat
            // 
            this.textBoxSendChat.Location = new System.Drawing.Point(551, 567);
            this.textBoxSendChat.Name = "textBoxSendChat";
            this.textBoxSendChat.Size = new System.Drawing.Size(156, 20);
            this.textBoxSendChat.TabIndex = 1;
            // 
            // textBoxChat
            // 
            this.textBoxChat.Location = new System.Drawing.Point(551, 82);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.ReadOnly = true;
            this.textBoxChat.Size = new System.Drawing.Size(237, 476);
            this.textBoxChat.TabIndex = 2;
            // 
            // buttonSendChat
            // 
            this.buttonSendChat.Location = new System.Drawing.Point(713, 564);
            this.buttonSendChat.Name = "buttonSendChat";
            this.buttonSendChat.Size = new System.Drawing.Size(75, 23);
            this.buttonSendChat.TabIndex = 3;
            this.buttonSendChat.Text = "Send";
            this.buttonSendChat.UseVisualStyleBackColor = true;
            // 
            // labelCurrentSpeed
            // 
            this.labelCurrentSpeed.AutoSize = true;
            this.labelCurrentSpeed.Location = new System.Drawing.Point(232, 44);
            this.labelCurrentSpeed.Name = "labelCurrentSpeed";
            this.labelCurrentSpeed.Size = new System.Drawing.Size(81, 13);
            this.labelCurrentSpeed.TabIndex = 4;
            this.labelCurrentSpeed.Text = "Current Speed: ";
            // 
            // labelCurrentSpeedValue
            // 
            this.labelCurrentSpeedValue.AutoSize = true;
            this.labelCurrentSpeedValue.Location = new System.Drawing.Point(375, 44);
            this.labelCurrentSpeedValue.Name = "labelCurrentSpeedValue";
            this.labelCurrentSpeedValue.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentSpeedValue.TabIndex = 5;
            this.labelCurrentSpeedValue.Text = "Waiting for value";
            // 
            // labelCurrentHearthbeat
            // 
            this.labelCurrentHearthbeat.AutoSize = true;
            this.labelCurrentHearthbeat.Location = new System.Drawing.Point(232, 149);
            this.labelCurrentHearthbeat.Name = "labelCurrentHearthbeat";
            this.labelCurrentHearthbeat.Size = new System.Drawing.Size(97, 13);
            this.labelCurrentHearthbeat.TabIndex = 6;
            this.labelCurrentHearthbeat.Text = "Current Heartbeat: ";
            // 
            // labelCurrentHearthbeatValue
            // 
            this.labelCurrentHearthbeatValue.AutoSize = true;
            this.labelCurrentHearthbeatValue.Location = new System.Drawing.Point(375, 149);
            this.labelCurrentHearthbeatValue.Name = "labelCurrentHearthbeatValue";
            this.labelCurrentHearthbeatValue.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentHearthbeatValue.TabIndex = 7;
            this.labelCurrentHearthbeatValue.Text = "Waiting for value";
            // 
            // labelCurrentResistance
            // 
            this.labelCurrentResistance.AutoSize = true;
            this.labelCurrentResistance.Location = new System.Drawing.Point(232, 244);
            this.labelCurrentResistance.Name = "labelCurrentResistance";
            this.labelCurrentResistance.Size = new System.Drawing.Size(103, 13);
            this.labelCurrentResistance.TabIndex = 8;
            this.labelCurrentResistance.Text = "Current Resistance: ";
            // 
            // labelCurrentResistanceValue
            // 
            this.labelCurrentResistanceValue.AutoSize = true;
            this.labelCurrentResistanceValue.Location = new System.Drawing.Point(375, 244);
            this.labelCurrentResistanceValue.Name = "labelCurrentResistanceValue";
            this.labelCurrentResistanceValue.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentResistanceValue.TabIndex = 9;
            this.labelCurrentResistanceValue.Text = "Waiting for value";
            // 
            // labelCurrentDistance
            // 
            this.labelCurrentDistance.AutoSize = true;
            this.labelCurrentDistance.Location = new System.Drawing.Point(232, 347);
            this.labelCurrentDistance.Name = "labelCurrentDistance";
            this.labelCurrentDistance.Size = new System.Drawing.Size(137, 13);
            this.labelCurrentDistance.TabIndex = 10;
            this.labelCurrentDistance.Text = "Current Distance Traveled: ";
            this.labelCurrentDistance.Click += new System.EventHandler(this.labelCurrentDistance_Click);
            // 
            // labelCurrentDistanceValue
            // 
            this.labelCurrentDistanceValue.AutoSize = true;
            this.labelCurrentDistanceValue.Location = new System.Drawing.Point(375, 347);
            this.labelCurrentDistanceValue.Name = "labelCurrentDistanceValue";
            this.labelCurrentDistanceValue.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentDistanceValue.TabIndex = 11;
            this.labelCurrentDistanceValue.Text = "Waiting for value";
            // 
            // labelCurrentPower
            // 
            this.labelCurrentPower.AutoSize = true;
            this.labelCurrentPower.Location = new System.Drawing.Point(232, 446);
            this.labelCurrentPower.Name = "labelCurrentPower";
            this.labelCurrentPower.Size = new System.Drawing.Size(80, 13);
            this.labelCurrentPower.TabIndex = 12;
            this.labelCurrentPower.Text = "Current Power: ";
            this.labelCurrentPower.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelCurrentPowerValue
            // 
            this.labelCurrentPowerValue.AutoSize = true;
            this.labelCurrentPowerValue.Location = new System.Drawing.Point(375, 446);
            this.labelCurrentPowerValue.Name = "labelCurrentPowerValue";
            this.labelCurrentPowerValue.Size = new System.Drawing.Size(87, 13);
            this.labelCurrentPowerValue.TabIndex = 13;
            this.labelCurrentPowerValue.Text = "Waiting for value";
            // 
            // groupBoxCurrentData
            // 
            this.groupBoxCurrentData.Controls.Add(this.powerGraph);
            this.groupBoxCurrentData.Controls.Add(this.distanceGraph);
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
            this.groupBoxCurrentData.Location = new System.Drawing.Point(17, 73);
            this.groupBoxCurrentData.Name = "groupBoxCurrentData";
            this.groupBoxCurrentData.Size = new System.Drawing.Size(512, 520);
            this.groupBoxCurrentData.TabIndex = 14;
            this.groupBoxCurrentData.TabStop = false;
            this.groupBoxCurrentData.Text = "Current Data";
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(26, 648);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(104, 23);
            this.buttonStartStop.TabIndex = 15;
            this.buttonStartStop.Text = "Start/Stop training";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // buttonNoodstop
            // 
            this.buttonNoodstop.Location = new System.Drawing.Point(463, 648);
            this.buttonNoodstop.Name = "buttonNoodstop";
            this.buttonNoodstop.Size = new System.Drawing.Size(75, 23);
            this.buttonNoodstop.TabIndex = 16;
            this.buttonNoodstop.Text = "Noodstop";
            this.buttonNoodstop.UseVisualStyleBackColor = true;
            // 
            // labelRestistance
            // 
            this.labelRestistance.AutoSize = true;
            this.labelRestistance.Location = new System.Drawing.Point(23, 621);
            this.labelRestistance.Name = "labelRestistance";
            this.labelRestistance.Size = new System.Drawing.Size(80, 13);
            this.labelRestistance.TabIndex = 17;
            this.labelRestistance.Text = "Resistance(%): ";
            // 
            // textBoxResistance
            // 
            this.textBoxResistance.Location = new System.Drawing.Point(109, 618);
            this.textBoxResistance.Name = "textBoxResistance";
            this.textBoxResistance.Size = new System.Drawing.Size(100, 20);
            this.textBoxResistance.TabIndex = 18;
            // 
            // trackBarResistance
            // 
            this.trackBarResistance.Location = new System.Drawing.Point(225, 609);
            this.trackBarResistance.Name = "trackBarResistance";
            this.trackBarResistance.Size = new System.Drawing.Size(104, 45);
            this.trackBarResistance.TabIndex = 19;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(222, 57);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(73, 13);
            this.labelTime.TabIndex = 20;
            this.labelTime.Text = "Training time: ";
            // 
            // labelTimeValue
            // 
            this.labelTimeValue.AutoSize = true;
            this.labelTimeValue.Location = new System.Drawing.Point(291, 57);
            this.labelTimeValue.Name = "labelTimeValue";
            this.labelTimeValue.Size = new System.Drawing.Size(87, 13);
            this.labelTimeValue.TabIndex = 21;
            this.labelTimeValue.Text = "Waiting for value";
            this.labelTimeValue.UseWaitCursor = true;
            // 
            // speedGraph
            // 
            this.speedGraph.BackColor = System.Drawing.Color.Transparent;
            this.speedGraph.CausesValidation = false;
            chartArea5.AxisX.MajorGrid.Enabled = false;
            chartArea5.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea5.Name = "ChartArea1";
            this.speedGraph.ChartAreas.Add(chartArea5);
            legend5.Enabled = false;
            legend5.Name = "Legend1";
            this.speedGraph.Legends.Add(legend5);
            this.speedGraph.Location = new System.Drawing.Point(-20, 9);
            this.speedGraph.Name = "speedGraph";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.speedGraph.Series.Add(series5);
            this.speedGraph.Size = new System.Drawing.Size(217, 104);
            this.speedGraph.TabIndex = 14;
            this.speedGraph.Text = "speedGraph";
            this.speedGraph.Click += new System.EventHandler(this.speedGraph_Click);
            // 
            // heartbeatGraph
            // 
            this.heartbeatGraph.BackColor = System.Drawing.Color.Transparent;
            this.heartbeatGraph.CausesValidation = false;
            chartArea4.AxisX.MajorGrid.Enabled = false;
            chartArea4.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea4.Name = "ChartArea1";
            this.heartbeatGraph.ChartAreas.Add(chartArea4);
            legend4.Enabled = false;
            legend4.Name = "Legend1";
            this.heartbeatGraph.Legends.Add(legend4);
            this.heartbeatGraph.Location = new System.Drawing.Point(-20, 110);
            this.heartbeatGraph.Name = "heartbeatGraph";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.heartbeatGraph.Series.Add(series4);
            this.heartbeatGraph.Size = new System.Drawing.Size(217, 104);
            this.heartbeatGraph.TabIndex = 15;
            this.heartbeatGraph.Text = "chart1";
            // 
            // resistanceGraph
            // 
            this.resistanceGraph.BackColor = System.Drawing.Color.Transparent;
            this.resistanceGraph.CausesValidation = false;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.Name = "ChartArea1";
            this.resistanceGraph.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.resistanceGraph.Legends.Add(legend3);
            this.resistanceGraph.Location = new System.Drawing.Point(-20, 206);
            this.resistanceGraph.Name = "resistanceGraph";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.resistanceGraph.Series.Add(series3);
            this.resistanceGraph.Size = new System.Drawing.Size(217, 104);
            this.resistanceGraph.TabIndex = 16;
            this.resistanceGraph.Text = "chart1";
            // 
            // distanceGraph
            // 
            this.distanceGraph.BackColor = System.Drawing.Color.Transparent;
            this.distanceGraph.CausesValidation = false;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.Name = "ChartArea1";
            this.distanceGraph.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.distanceGraph.Legends.Add(legend2);
            this.distanceGraph.Location = new System.Drawing.Point(-20, 305);
            this.distanceGraph.Name = "distanceGraph";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.distanceGraph.Series.Add(series2);
            this.distanceGraph.Size = new System.Drawing.Size(217, 104);
            this.distanceGraph.TabIndex = 17;
            this.distanceGraph.Text = "chart1";
            // 
            // powerGraph
            // 
            this.powerGraph.BackColor = System.Drawing.Color.Transparent;
            this.powerGraph.CausesValidation = false;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea1";
            this.powerGraph.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.powerGraph.Legends.Add(legend1);
            this.powerGraph.Location = new System.Drawing.Point(-20, 410);
            this.powerGraph.Name = "powerGraph";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.powerGraph.Series.Add(series1);
            this.powerGraph.Size = new System.Drawing.Size(217, 104);
            this.powerGraph.TabIndex = 18;
            this.powerGraph.Text = "chart2";
            // 
            // LiveSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 681);
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
            this.Name = "LiveSession";
            this.Text = "LiveSession";
            this.groupBoxCurrentData.ResumeLayout(false);
            this.groupBoxCurrentData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarResistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heartbeatGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resistanceGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerGraph)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart speedGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart powerGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart distanceGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart resistanceGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart heartbeatGraph;
    }
}