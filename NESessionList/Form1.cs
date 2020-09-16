using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NESessionList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] row0 = { "11/22/1968", "29" };
            dataGridView1.Rows.Add(row0);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
