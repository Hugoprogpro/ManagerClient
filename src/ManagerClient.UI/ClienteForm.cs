using System;
using System.Windows.Forms;
using ManagerClient.Domain;
using ManagerCliente.Infra;

namespace ManagerClient.UI
{
    public partial class ClienteForm : Form
    {
        const string connectionString =
            @"Server=CLEIVIANE-PC\CURSO;Database=DBManagerClient;User ID=sa;Password=sap@123;Trusted_Connection=False;";

        private static readonly ITodosClientes _todosClientes = new TodosClientesBanco(connectionString);
        private readonly ClienteServico _clientService = new ClienteServico(_todosClientes);

        private readonly Cliente _clienteAtual;

        public ClienteForm(FormType.FormMode pFormMode, Cliente cliente)
        {
            _clienteAtual = cliente;

            InitializeComponent();
            InicializarFormulario(pFormMode);
        }

        private void InicializarFormulario(FormType.FormMode pFormMode)
        {
            PreencherCampos();
            PreencherCidades();

            ConfigurarEventos();

            if (pFormMode == FormType.FormMode.AddMode)
                txtCodigo.Text = _clientService.GetNextSequenceNumber().ToString();

            if (pFormMode == FormType.FormMode.EditMode)
                FillCliente();
        }

        private void PreencherCidades()
        {
            var cidades = new CidadeBanco(connectionString);
            cboCidade.Sorted = true;

            cboCidade.DataSource = cidades.ObterListatodos();

            cboCidade.DisplayMember = "Descricao";
            cboCidade.ValueMember = "Id";
        }

        private void FillCliente()
        {
            txtNome.Text = _clienteAtual.Nome;
            txtBairro.Text = _clienteAtual.Endereco.Bairro;
            txtCodigo.Text = _clienteAtual.Codigo.ToString();
            txtDataCadastro.Text = _clienteAtual.DataCadastro.ToString();
            txtLogradouro.Text = _clienteAtual.Endereco.Logradouro;
            txtNumero.Text = _clienteAtual.Endereco.Numero;
            txtTelefone.Text = _clienteAtual.Telefone;
        }

        private void PreencherCampos()
        {
            txtCodigo.Enabled = false;
            txtDataCadastro.Enabled = false;
            txtDataCadastro.Text = DateTime.Now.ToString();
        }

        private void ConfigurarEventos()
        {
            okButton.Click += okButton_Click;
            
            CancelButton.Click += CancelButton_Click;
            txtCodigo.LostFocus += codeTextBox_LostFocus;
        }

        void codeTextBox_LostFocus(object sender, EventArgs e)
        {
            var cliente = _clientService.GetByKey(Convert.ToInt32(txtCodigo.Text));

            if (cliente != null)
                FillControls(cliente);
        }

        private void FillControls(Cliente cliente)
        {
            txtNome.Text = cliente.Nome;
            txtDataCadastro.Text = cliente.DataCadastro.ToString();
        }

        void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        void okButton_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente
                              {
                                  Nome = txtNome.Text,
                                  Codigo = GetNextCode(),
                                  Telefone = txtTelefone.Text,
                                  DataCadastro = Convert.ToDateTime(txtDataCadastro.Text)
                              };

            cliente.Endereco.Bairro = txtBairro.Text;
            cliente.Endereco.Cidade = cboCidade.SelectedIndex;
            cliente.Endereco.Logradouro = txtLogradouro.Text;
            cliente.Endereco.Numero = txtNumero.Text;

            _clientService.Salvar(cliente);

            MessageBox.Show("Cadastro adicionado com sucesso!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Close();
        }

        private int GetNextCode()
        {
            var quantTodosClientes = _todosClientes.ObterListatodos().Count;
            return quantTodosClientes == 0 ? 1: quantTodosClientes + 1;
        }
    }
}
