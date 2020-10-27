using ClientApplication;
using ClientApplication.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class LoginForm : Form
    {
        private Client client;
        public LoginForm()
        {
            InitializeComponent();
            this.client = new Client();
            this.client.OnLogin += Client_OnLogin;
        }

        private void Client_OnLogin(bool status)
        {
            this.Invoke((Action)delegate {
                if (status)
                {
                    Form1 VRForm = new Form1(this.client);
                    VRForm.Show();
                    //MainForm mainForm = new MainForm(this.client);
                    //mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Password or Username", "Error");
                }
            });
            
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (NameTextBox.Text != null || PasswordTextBox.Text != null)
            {
                client.SendLogin(NameTextBox.Text, PasswordTextBox.Text);
            }
        }

        private void LoginButton_Click()
        {
            if (NameTextBox.Text != null || PasswordTextBox.Text != null)
            {
                client.SendLogin(NameTextBox.Text, PasswordTextBox.Text);
            }
        }

        private void NameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoginButton_Click();
            }
        }

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoginButton_Click();
            }
        }
    }
}
