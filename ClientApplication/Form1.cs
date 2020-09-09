using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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

        private void trackBarSpeed_Scroll(object sender, EventArgs e)
        {
            textBoxSpeed.Text = "" + trackBarSpeed.Value;
        }

        private void trackBarHeartbeat_Scroll(object sender, EventArgs e)
        {
            textBoxHeartbeat.Text = "" + trackBarHeartbeat.Value;
        }

        private void textBoxSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBoxSpeed.Text))
                {
                    try
                    {
                        if (int.Parse(textBoxSpeed.Text) >= trackBarSpeed.Minimum && int.Parse(textBoxSpeed.Text) <= trackBarSpeed.Maximum)
                        {
                            trackBarSpeed.Value = int.Parse(textBoxSpeed.Text);
                        }
                        else if (int.Parse(textBoxSpeed.Text) > trackBarSpeed.Maximum)
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
                    catch
                    {
                        textBoxSpeed.Text = trackBarSpeed.Value + "";

                    }
                }
                else
                {
                    textBoxSpeed.Text = trackBarSpeed.Minimum + "";
                    trackBarSpeed.Value = trackBarSpeed.Minimum;
                }

            }
        }

        private void textBoxHeartbeat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBoxHeartbeat.Text))
                {
                    try
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
                    catch
                    {
                        textBoxHeartbeat.Text = trackBarHeartbeat.Value + "";

                    }
                }
                else
                {
                    textBoxHeartbeat.Text = trackBarHeartbeat.Minimum + "";
                    trackBarHeartbeat.Value = trackBarHeartbeat.Minimum;
                }

            }
        }
    }
}
