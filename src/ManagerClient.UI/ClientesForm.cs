using System;
using System.Windows.Forms;
using ManagerClient.Domain;
using ManagerCliente.Infra;

namespace ManagerClient.UI
{
    public partial class ClientsForm : Form
    {
        const string connectionString = @"Server=localhost\CURSO;Database=DBManagerClient;Trusted_Connection=true;";
        private ClienteServico _clientService;

        public ClientsForm()
        {
            InitializeComponent();
            CriarClienteService();

            InitializeForm();
        }

        private void PreencherClientes()
        {
            clientsGrid.DataSource = _clientService.Obtertodos();
            clientsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CriarClienteService()
        {
            var _todosClientes = new TodosClientesBanco(connectionString);
            _clientService = new ClienteServico(_todosClientes);
        }

        private void InitializeForm()
        {
            SetEvents();
            PreencherClientes();
        }

        private void FormatCliente()
        {
            clientsGrid.Columns.Add("Codigo", "Código");
            clientsGrid.Columns.Add("Nome", "Nome");
            clientsGrid.Columns.Add("DataCadastro", "Cadastro");
            clientsGrid.Columns.Add("Telefone", "Telefone");
            clientsGrid.Columns.Add("Logradouro", "Endereço");
            clientsGrid.Columns.Add("Bairro", "Bairro");
            clientsGrid.Columns.Add("Numero", "Número");
            clientsGrid.Columns.Add("Cidade", "Cidade");
        }

        private void SetEvents()
        {
            fecharButton.Click += fecharButton_Click;
            newButton.Click += newButton_Click;
            excluirButton.Click += excluirButton_Click;
            alterarButton.Click += alterarButton_Click;

            searchTxt.KeyPress += searchTxt_KeyPress;
        }

        void alterarButton_Click(object sender, EventArgs e)
        {
            
        }

        void excluirButton_Click(object sender, EventArgs e)
        {
            
        }

        void fecharButton_Click(object sender, EventArgs e)
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

        void newButton_Click(object sender, EventArgs e)
        {
            var form = new ClienteForm(FormType.FormMode.AddMode);
            form.ShowDialog();

            PreencherClientes();
        }

        void searchTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                var clientName = searchTxt.Text;
                var findClients = _clientService.GetByName(clientName);

                clientsGrid.DataSource = findClients;
            }
        }
    }
}
