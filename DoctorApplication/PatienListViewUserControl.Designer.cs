namespace DoctorApplication
{
    partial class PatienListViewUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EmergencyShutdownButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UserControlNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCurrentHearthbeatValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EmergencyShutdownButton
            // 
            this.EmergencyShutdownButton.Location = new System.Drawing.Point(0, 95);
            this.EmergencyShutdownButton.Name = "EmergencyShutdownButton";
            this.EmergencyShutdownButton.Size = new System.Drawing.Size(155, 32);
            this.EmergencyShutdownButton.TabIndex = 0;
            this.EmergencyShutdownButton.Text = "Emergency Shutdown";
            this.EmergencyShutdownButton.UseVisualStyleBackColor = true;
            this.EmergencyShutdownButton.Click += new System.EventHandler(this.EmergencyShutdownButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseCompatibleTextRendering = true;
            // 
            // UserControlNameLabel
            // 
            this.UserControlNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UserControlNameLabel.AutoSize = true;
            this.UserControlNameLabel.Location = new System.Drawing.Point(53, 24);
            this.UserControlNameLabel.Name = "UserControlNameLabel";
            this.UserControlNameLabel.Size = new System.Drawing.Size(48, 15);
            this.UserControlNameLabel.TabIndex = 2;
            this.UserControlNameLabel.Text = "Waiting";
            this.UserControlNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UserControlNameLabel.Click += new System.EventHandler(this.UserControlNameLabel_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Current hearbeat:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCurrentHearthbeatValue
            // 
            this.labelCurrentHearthbeatValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCurrentHearthbeatValue.AutoSize = true;
            this.labelCurrentHearthbeatValue.Location = new System.Drawing.Point(53, 71);
            this.labelCurrentHearthbeatValue.Name = "labelCurrentHearthbeatValue";
            this.labelCurrentHearthbeatValue.Size = new System.Drawing.Size(48, 15);
            this.labelCurrentHearthbeatValue.TabIndex = 4;
            this.labelCurrentHearthbeatValue.Text = "Waiting";
            this.labelCurrentHearthbeatValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatienListViewUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelCurrentHearthbeatValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.UserControlNameLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EmergencyShutdownButton);
            this.Name = "PatienListViewUserControl";
            this.Size = new System.Drawing.Size(155, 134);
            this.Load += new System.EventHandler(this.PatienListViewUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EmergencyShutdownButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label UserControlNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCurrentHearthbeatValue;
    }
}
