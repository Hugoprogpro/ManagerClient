using System.Windows.Forms;

namespace ManagerClient.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Initializeform();
        }

        private void Initializeform()
        {
            SetEvents();
        }

        private void SetEvents()
        {
            ClienteMenu.Click += ClienteMenu_Click;
        }

        void ClienteMenu_Click(object sender, System.EventArgs e)
        {
            var form = new ClientsForm();
            form.ShowDialog();
        }
    }
}
