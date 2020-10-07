using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            bool correct = true;
            if (correct)
            {
                HomePageForm homePage = new HomePageForm();
                homePage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Password or Username", "Error");
            }
        }
    }
}
