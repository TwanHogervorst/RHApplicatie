namespace DoctorApplication
{
    partial class HistoryForm
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
            this.trainingListView = new System.Windows.Forms.ListView();
            this.trainingDataTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // trainingListView
            // 
            this.trainingListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.trainingListView.HideSelection = false;
            this.trainingListView.Location = new System.Drawing.Point(2, 2);
            this.trainingListView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trainingListView.Name = "trainingListView";
            this.trainingListView.Size = new System.Drawing.Size(140, 515);
            this.trainingListView.TabIndex = 0;
            this.trainingListView.UseCompatibleStateImageBehavior = false;
            // 
            // trainingDataTextBox
            // 
            this.trainingDataTextBox.Location = new System.Drawing.Point(149, 2);
            this.trainingDataTextBox.Multiline = true;
            this.trainingDataTextBox.Name = "trainingDataTextBox";
            this.trainingDataTextBox.Size = new System.Drawing.Size(660, 515);
            this.trainingDataTextBox.TabIndex = 1;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 519);
            this.Controls.Add(this.trainingDataTextBox);
            this.Controls.Add(this.trainingListView);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "HistoryForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Text = "History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HistoryForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView trainingListView;
        private System.Windows.Forms.TextBox trainingDataTextBox;
    }
}