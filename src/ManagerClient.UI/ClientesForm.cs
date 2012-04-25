using System;
using System.Windows.Forms;
using ManagerClient.Domain;
using ManagerCliente.Infra;

namespace ManagerClient.UI
{
    public partial class ClientsForm : Form
    {
        const string connectionString =
            @"Server=CLEIVIANE-PC\TESTE;Database=DBManagerClient;User ID=sa;Password=sap@123;Trusted_Connection=False;";

        private ClienteServico _clientService;
        private Cliente currCliente = new Cliente();

        public ClientsForm()
        {
            InitializeComponent();
            CriarClienteService();

            InicializarFormulario();
        }

        private void CriarClienteService()
        {
            var _todosClientes = new TodosClientesBanco(connectionString);
            _clientService = new ClienteServico(_todosClientes);
        }

        private void InicializarFormulario()
        {
            ConfigurarEventos();
            PreencherClientes();
        }

        private void ConfigurarEventos()
        {
            fecharButton.Click += fecharButton_Click;
            newButton.Click += newButton_Click;
            excluirButton.Click += excluirButton_Click;
            alterarButton.Click += alterarButton_Click;
            
            searchTxt.KeyPress += searchTxt_KeyPress;
        }

        private void PreencherClientes()
        {
            clientsGrid.DataSource = _clientService.Obtertodos();
            clientsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        void alterarButton_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente();
            var linhasSelecionadas = clientsGrid.SelectedRows;

            cliente.Codigo = int.Parse(linhasSelecionadas[0].Cells["Codigo"].Value.ToString());
            cliente.DataCadastro = DateTime.Parse(linhasSelecionadas[0].Cells["DataCadastro"].Value.ToString());
            cliente.Endereco.Bairro = linhasSelecionadas[0].Cells["Bairro"].Value.ToString();
            cliente.Endereco.Cidade = int.Parse(linhasSelecionadas[0].Cells["Cidade"].Value.ToString());
            cliente.Endereco.Logradouro = linhasSelecionadas[0].Cells["Logradouro"].Value.ToString();
            cliente.Endereco.Numero = linhasSelecionadas[0].Cells["Numero"].Value.ToString();
            cliente.Nome = linhasSelecionadas[0].Cells["Nome"].Value.ToString();
            cliente.Telefone = linhasSelecionadas[0].Cells["Telefone"].Value.ToString();

            var form = new ClienteForm(FormType.FormMode.EditMode, cliente);
            form.Show();
        }

        void excluirButton_Click(object sender, EventArgs e)
        {
            var clienteSelecionado = clientsGrid.SelectedRows;
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
            var form = new ClienteForm(FormType.FormMode.AddMode, null);
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
