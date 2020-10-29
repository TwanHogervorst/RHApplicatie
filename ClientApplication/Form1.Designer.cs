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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.continueWithoutVR = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(35, 529);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(125, 41);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.MinimumWidth = 6;
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NameColumn.Width = 200;
            // 
            // IDColumn
            // 
            this.IDColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.MinimumWidth = 6;
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            this.IDColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.IDColumn});
            this.dataGridView1.Location = new System.Drawing.Point(35, 36);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(840, 253);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.Text = "dataGridView1";
            // 
            // continueWithoutVR
            // 
            this.continueWithoutVR.Location = new System.Drawing.Point(167, 529);
            this.continueWithoutVR.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.continueWithoutVR.Name = "continueWithoutVR";
            this.continueWithoutVR.Size = new System.Drawing.Size(168, 41);
            this.continueWithoutVR.TabIndex = 3;
            this.continueWithoutVR.Text = "Continue without VR";
            this.continueWithoutVR.UseVisualStyleBackColor = true;
            this.continueWithoutVR.Click += new System.EventHandler(this.continueWithoutVR_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 320);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Key ";
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(75, 316);
            this.keyTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(114, 27);
            this.keyTextBox.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.continueWithoutVR);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ConnectButton);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button continueWithoutVR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox keyTextBox;
    }
}

