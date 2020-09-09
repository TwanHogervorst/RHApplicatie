using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void labelResistance_Click(object sender, EventArgs e)
        {

        }

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxSpeed.Text = "" + trackBarSpeed.Value;
        }

        private void trackBarHeartbeat_Scroll(object sender, EventArgs e)
        {
            textBoxHeartbeat.Text = "" + trackBarHeartbeat.Value;
        }

        private void textBoxSpeed_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSpeed.Text))
            {
                if (int.Parse(textBoxSpeed.Text) >= trackBarSpeed.Minimum && int.Parse(textBoxSpeed.Text) <= trackBarSpeed.Maximum)
                {
                    trackBarSpeed.Value = int.Parse(textBoxSpeed.Text);
                }
                else if(int.Parse(textBoxSpeed.Text) > trackBarSpeed.Maximum)
                {
                    textBoxSpeed.Text = trackBarSpeed.Maximum + "";
                    trackBarSpeed.Value = trackBarSpeed.Maximum;
                }
                else if (int.Parse(textBoxSpeed.Text) < trackBarSpeed.Minimum)
                {
                    textBoxSpeed.Text = trackBarSpeed.Minimum + "";
                    trackBarSpeed.Value = trackBarSpeed.Minimum;
                }
            }
        }

        private void textBoxHeartbeat_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxHeartbeat.Text))
            {
                if (int.Parse(textBoxHeartbeat.Text) >= trackBarHeartbeat.Minimum && int.Parse(textBoxHeartbeat.Text) <= trackBarHeartbeat.Maximum)
                {
                    trackBarHeartbeat.Value = int.Parse(textBoxHeartbeat.Text);
                }
                else if (int.Parse(textBoxHeartbeat.Text) > trackBarHeartbeat.Maximum)
                {
                    textBoxHeartbeat.Text = trackBarHeartbeat.Maximum + "";
                    trackBarHeartbeat.Value = trackBarHeartbeat.Maximum;
                }
                else if (int.Parse(textBoxHeartbeat.Text) < trackBarHeartbeat.Minimum)
                {
                    textBoxHeartbeat.Text = trackBarHeartbeat.Minimum + "";
                    trackBarHeartbeat.Value = trackBarHeartbeat.Minimum;
                }
            }
        }

        private void textBoxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void textBoxHeartbeat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }
    }
}
