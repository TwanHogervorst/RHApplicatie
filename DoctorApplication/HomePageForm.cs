using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class HomePageForm : Form
    {

        public HomePageForm()
        {
            InitializeComponent();
        }

        private void LiveSessionButton_Click(object sender, System.EventArgs e)
        {
            LiveSession liveSession = new LiveSession(selectedUser);
            liveSession.Show();
        }

        private void HistoryButton_Click(object sender, System.EventArgs e)
        {
            HistoryForm historySession = new HistoryForm(selectedUser);
            historySession.Show();
        }

        private string selectedUser = "";

        private void PatientListView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListView listview = (ListView)sender;
            selectedUser = listview.SelectedItems[0].ToString();

        }
    }
}
