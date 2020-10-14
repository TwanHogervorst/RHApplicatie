using System.Windows.Forms;

namespace DoctorApplication
{
    public partial class HistoryForm : Form
    {
        private string selectedUser;
        public HistoryForm(string selected)
        {
            InitializeComponent();
            this.selectedUser = selected;
        }
        public HistoryForm()
        {
            InitializeComponent();
        }
    }
}
