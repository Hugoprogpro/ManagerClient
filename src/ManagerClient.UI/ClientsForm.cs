using System;
using System.Windows.Forms;
using ManagerClient.Domain;
using ManagerCliente.Infra;

namespace ManagerClient.UI
{
    public partial class ClientsForm : Form
    {
        public ITodosClientes _todosOsClientes { get; set; }
        const string connectionString = @"Server=localhost\Teste;Database=DBManagerClient;Trusted_Connection=true;";

        public ClientsForm()
        {
            _todosOsClientes = new TodosClientesBanco(connectionString);

            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            SetEvents();
            FillGrid();
        }

        private void FillGrid()
        {
            clientsGrid.DataSource = _todosOsClientes.Obtertodos();
        }

        private void SetEvents()
        {
            okButton.Click += okButton_Click;
            searchTxt.KeyPress += searchTxt_KeyPress;
            newButton.Click += newButton_Click;
        }

        void newButton_Click(object sender, EventArgs e)
        {
            var form = new ClienteForm(FormType.FormMode.AddMode);
            form.ShowDialog();
        }

        void searchTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                var clientName = searchTxt.Text;
                clientsGrid.DataSource = _todosOsClientes.GetByName(clientName);
            }
        }

        void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
