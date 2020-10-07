using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class LiveSession : Form
    {
        public LiveSession()
        {
            InitializeComponent();
        }

        private void labelCurrentDistance_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {

        }

        private void speedGraph_Click(object sender, EventArgs e)
        {
            speedGraph.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            speedGraph.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
        }
    }
}
