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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            bool correct = false;
            if (correct)
            {
                HomePageForm homePage = new HomePageForm();
                homePage.Show();
                this.Close();
            }
            else
            {
                //MessageBox.Show("Invalid Password or Username", "Error");
                Button button = (Button)sender;
                Color backColor = button.BackColor;
                Color foreColor = button.ForeColor;
                button.Text = "Invalid Password or Username";
                button.BackColor = Color.Red;
                button.ForeColor = Color.Red;
                Application.DoEvents();
                System.Threading.Thread.Sleep(750);
                button.Text = "Login";
                button.BackColor = backColor;
                button.ForeColor = foreColor;

            }
        }
    }
}
