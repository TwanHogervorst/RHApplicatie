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
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeartbeat)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(200, 181);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(147, 27);
            this.textBoxSpeed.TabIndex = 0;
            this.textBoxSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSpeed_KeyPress);
            // 
            // textBoxHeartbeat
            // 
            this.textBoxHeartbeat.Location = new System.Drawing.Point(475, 181);
            this.textBoxHeartbeat.Name = "textBoxHeartbeat";
            this.textBoxHeartbeat.Size = new System.Drawing.Size(147, 27);
            this.textBoxHeartbeat.TabIndex = 1;
            this.textBoxHeartbeat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHeartbeat_KeyPress);
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(245, 160);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(51, 20);
            this.labelSpeed.TabIndex = 3;
            this.labelSpeed.Text = "Speed";
            // 
            // labelHeartbeat
            // 
            this.labelHeartbeat.AutoSize = true;
            this.labelHeartbeat.Location = new System.Drawing.Point(531, 160);
            this.labelHeartbeat.Name = "labelHeartbeat";
            this.labelHeartbeat.Size = new System.Drawing.Size(39, 20);
            this.labelHeartbeat.TabIndex = 4;
            this.labelHeartbeat.Text = "BPM";
            // 
            // buttonSendData
            // 
            this.buttonSendData.Location = new System.Drawing.Point(347, 359);
            this.buttonSendData.Name = "buttonSendData";
            this.buttonSendData.Size = new System.Drawing.Size(94, 29);
            this.buttonSendData.TabIndex = 6;
            this.buttonSendData.Text = "Send Data";
            this.buttonSendData.UseVisualStyleBackColor = true;
            // 
            // trackBarSpeed
            // 
            this.trackBarSpeed.Location = new System.Drawing.Point(200, 226);
            this.trackBarSpeed.Maximum = 40;
            this.trackBarSpeed.Name = "trackBarSpeed";
            this.trackBarSpeed.Size = new System.Drawing.Size(147, 56);
            this.trackBarSpeed.TabIndex = 7;
            this.trackBarSpeed.Scroll += new System.EventHandler(this.trackBarSpeed_Scroll);
            // 
            // trackBarHeartbeat
            // 
            this.trackBarHeartbeat.Location = new System.Drawing.Point(475, 226);
            this.trackBarHeartbeat.Maximum = 230;
            this.trackBarHeartbeat.Minimum = 50;
            this.trackBarHeartbeat.Name = "trackBarHeartbeat";
            this.trackBarHeartbeat.Size = new System.Drawing.Size(147, 56);
            this.trackBarHeartbeat.TabIndex = 7;
            this.trackBarHeartbeat.Value = 50;
            this.trackBarHeartbeat.Scroll += new System.EventHandler(this.trackBarHeartbeat_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}

