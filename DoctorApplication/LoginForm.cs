using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class LoginForm : Form
    {
        private DoctorClient client;
        public LoginForm()
        {
            InitializeComponent();
            this.client = new DoctorClient();
            this.client.OnLogin += Client_OnLogin;
        }
        private void Client_OnLogin(bool status)
        {
            this.Invoke((Action)delegate {
                if (status)
                {

                    HomePageForm homePageForm = new HomePageForm(this.client);
                    homePageForm.Show();
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
    }
}
