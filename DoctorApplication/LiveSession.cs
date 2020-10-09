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
        private string selectedUser;
        private DoctorClient client;
        public LiveSession(DoctorClient client, String selected)
        {
            InitializeComponent();
            this.client = client;
            this.client.OnChatReceived += Client_OnChatReceived; ;
            this.selectedUser = selected;
            Patient.Text += selected;
        }

        private void Client_OnChatReceived(string message)
        {
            textBoxChat.Text += message;
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
            
        }

        private void LiveSession_Load(object sender, EventArgs e)
        {

        }
    }
}
