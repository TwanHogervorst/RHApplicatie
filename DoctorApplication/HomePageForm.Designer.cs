namespace DoctorApplication
{
    partial class HomePageForm
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
            this.PatientListView = new System.Windows.Forms.ListView();
            this.BroadcastContainer = new System.Windows.Forms.SplitContainer();
            this.BroadcastTextBox = new System.Windows.Forms.TextBox();
            this.BroadcastSendButton = new System.Windows.Forms.Button();
            this.BoradcastPanel = new System.Windows.Forms.Panel();
            this.PatientInformationGroupBox = new System.Windows.Forms.GroupBox();
            this.PatientButtonContainer = new System.Windows.Forms.SplitContainer();
            this.LiveSessionButton = new System.Windows.Forms.Button();
            this.HistoryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BroadcastContainer)).BeginInit();
            this.BroadcastContainer.Panel1.SuspendLayout();
            this.BroadcastContainer.Panel2.SuspendLayout();
            this.BroadcastContainer.SuspendLayout();
            this.BoradcastPanel.SuspendLayout();
            this.PatientInformationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PatientButtonContainer)).BeginInit();
            this.PatientButtonContainer.Panel1.SuspendLayout();
            this.PatientButtonContainer.Panel2.SuspendLayout();
            this.PatientButtonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatientListView
            // 
            this.PatientListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.PatientListView.HideSelection = false;
            this.PatientListView.Location = new System.Drawing.Point(6, 7);
            this.PatientListView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.PatientListView.MultiSelect = false;
            this.PatientListView.Name = "PatientListView";
            this.PatientListView.Size = new System.Drawing.Size(212, 706);
            this.PatientListView.TabIndex = 0;
            this.PatientListView.UseCompatibleStateImageBehavior = false;
            this.PatientListView.SelectedIndexChanged += new System.EventHandler(this.PatientListView_SelectedIndexChanged);
            // 
            // BroadcastContainer
            // 
            this.BroadcastContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BroadcastContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.BroadcastContainer.Location = new System.Drawing.Point(5, 0);
            this.BroadcastContainer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BroadcastContainer.Name = "BroadcastContainer";
            // 
            // BroadcastContainer.Panel1
            // 
            this.BroadcastContainer.Panel1.Controls.Add(this.BroadcastTextBox);
            // 
            // BroadcastContainer.Panel2
            // 
            this.BroadcastContainer.Panel2.Controls.Add(this.BroadcastSendButton);
            this.BroadcastContainer.Size = new System.Drawing.Size(641, 71);
            this.BroadcastContainer.SplitterDistance = 533;
            this.BroadcastContainer.SplitterWidth = 6;
            this.BroadcastContainer.TabIndex = 1;
            // 
            // BroadcastTextBox
            // 
            this.BroadcastTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BroadcastTextBox.Location = new System.Drawing.Point(0, 0);
            this.BroadcastTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BroadcastTextBox.Multiline = true;
            this.BroadcastTextBox.Name = "BroadcastTextBox";
            this.BroadcastTextBox.Size = new System.Drawing.Size(533, 71);
            this.BroadcastTextBox.TabIndex = 0;
            this.BroadcastTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BroadcastTextBox_KeyPress);
            // 
            // BroadcastSendButton
            // 
            this.BroadcastSendButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BroadcastSendButton.Location = new System.Drawing.Point(0, 0);
            this.BroadcastSendButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BroadcastSendButton.Name = "BroadcastSendButton";
            this.BroadcastSendButton.Size = new System.Drawing.Size(102, 71);
            this.BroadcastSendButton.TabIndex = 0;
            this.BroadcastSendButton.Text = "Send Broadcast";
            this.BroadcastSendButton.UseVisualStyleBackColor = true;
            this.BroadcastSendButton.Click += new System.EventHandler(this.BroadcastSendButton_Click);
            // 
            // BoradcastPanel
            // 
            this.BoradcastPanel.Controls.Add(this.BroadcastContainer);
            this.BoradcastPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BoradcastPanel.Location = new System.Drawing.Point(218, 7);
            this.BoradcastPanel.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.BoradcastPanel.Name = "BoradcastPanel";
            this.BoradcastPanel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.BoradcastPanel.Size = new System.Drawing.Size(646, 71);
            this.BoradcastPanel.TabIndex = 2;
            // 
            // PatientInformationGroupBox
            // 
            this.PatientInformationGroupBox.Controls.Add(this.PatientButtonContainer);
            this.PatientInformationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PatientInformationGroupBox.Location = new System.Drawing.Point(218, 78);
            this.PatientInformationGroupBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.PatientInformationGroupBox.Name = "PatientInformationGroupBox";
            this.PatientInformationGroupBox.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.PatientInformationGroupBox.Size = new System.Drawing.Size(646, 635);
            this.PatientInformationGroupBox.TabIndex = 3;
            this.PatientInformationGroupBox.TabStop = false;
            this.PatientInformationGroupBox.Text = "Patient Information";
            // 
            // PatientButtonContainer
            // 
            this.PatientButtonContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PatientButtonContainer.Location = new System.Drawing.Point(5, 563);
            this.PatientButtonContainer.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.PatientButtonContainer.Name = "PatientButtonContainer";
            // 
            // PatientButtonContainer.Panel1
            // 
            this.PatientButtonContainer.Panel1.Controls.Add(this.LiveSessionButton);
            // 
            // PatientButtonContainer.Panel2
            // 
            this.PatientButtonContainer.Panel2.Controls.Add(this.HistoryButton);
            this.PatientButtonContainer.Size = new System.Drawing.Size(636, 68);
            this.PatientButtonContainer.SplitterDistance = 301;
            this.PatientButtonContainer.SplitterWidth = 6;
            this.PatientButtonContainer.TabIndex = 0;
            // 
            // LiveSessionButton
            // 
            this.LiveSessionButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.LiveSessionButton.Location = new System.Drawing.Point(0, 0);
            this.LiveSessionButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.LiveSessionButton.Name = "LiveSessionButton";
            this.LiveSessionButton.Size = new System.Drawing.Size(101, 68);
            this.LiveSessionButton.TabIndex = 0;
            this.LiveSessionButton.Text = "Live Session";
            this.LiveSessionButton.UseVisualStyleBackColor = true;
            this.LiveSessionButton.Click += new System.EventHandler(this.LiveSessionButton_Click);
            // 
            // HistoryButton
            // 
            this.HistoryButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.HistoryButton.Location = new System.Drawing.Point(228, 0);
            this.HistoryButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.HistoryButton.Name = "HistoryButton";
            this.HistoryButton.Size = new System.Drawing.Size(101, 68);
            this.HistoryButton.TabIndex = 0;
            this.HistoryButton.Text = "History";
            this.HistoryButton.UseVisualStyleBackColor = true;
            this.HistoryButton.Click += new System.EventHandler(this.HistoryButton_Click);
            // 
            // HomePageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 720);
            this.Controls.Add(this.PatientInformationGroupBox);
            this.Controls.Add(this.BoradcastPanel);
            this.Controls.Add(this.PatientListView);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "HomePageForm";
            this.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Text = "HomePage";
            this.BroadcastContainer.Panel1.ResumeLayout(false);
            this.BroadcastContainer.Panel1.PerformLayout();
            this.BroadcastContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BroadcastContainer)).EndInit();
            this.BroadcastContainer.ResumeLayout(false);
            this.BoradcastPanel.ResumeLayout(false);
            this.PatientInformationGroupBox.ResumeLayout(false);
            this.PatientButtonContainer.Panel1.ResumeLayout(false);
            this.PatientButtonContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PatientButtonContainer)).EndInit();
            this.PatientButtonContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PatientListView;
        private System.Windows.Forms.SplitContainer BroadcastContainer;
        private System.Windows.Forms.TextBox BroadcastTextBox;
        private System.Windows.Forms.Button BroadcastSendButton;
        private System.Windows.Forms.Panel BoradcastPanel;
        private System.Windows.Forms.GroupBox PatientInformationGroupBox;
        private System.Windows.Forms.SplitContainer PatientButtonContainer;
        private System.Windows.Forms.Button LiveSessionButton;
        private System.Windows.Forms.Button HistoryButton;
    }
}