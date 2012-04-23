using System;
using System.Windows.Forms;
using ManagerClient.Domain;
using ManagerCliente.Infra;

namespace ManagerClient.UI
{
    public partial class ClienteForm : Form
    {
        const string connectionString = @"Server=localhost\CURSO;Database=DBManagerClient;Trusted_Connection=true;";
        private static readonly ITodosClientes _todosClientes = new TodosClientesBanco(connectionString);
        private readonly ClienteServico _clientService = new ClienteServico(_todosClientes);

        public ClienteForm(FormType.FormMode pFormMode)
        {
            InitializeComponent();
            InitializeForm(pFormMode);
        }

        private void InitializeForm(FormType.FormMode pFormMode)
        {
            FillControls();

            SetEvents();

            if (pFormMode == FormType.FormMode.AddMode)
                codeTextBox.Text = _clientService.GetNextSequenceNumber().ToString();

            if (pFormMode == FormType.FormMode.EditMode)
                FillCliente();
        }

        private void FillCliente()
        {
            
        }

        private void FillControls()
        {
            codeTextBox.Enabled = false;
            DataCadastroTextBox.Enabled = false;
            DataCadastroTextBox.Text = DateTime.Now.ToString();
        }

        private void SetEvents()
        {
            okButton.Click += okButton_Click;
            CancelButton.Click += CancelButton_Click;
            codeTextBox.LostFocus += codeTextBox_LostFocus;
        }

        void codeTextBox_LostFocus(object sender, EventArgs e)
        {
            var cliente = _clientService.GetByKey(Convert.ToInt32(codeTextBox.Text));

            if (cliente != null)
                FillControls(cliente);
        }

        private void FillControls(Cliente cliente)
        {
            NomeTextBox.Text = cliente.Nome;
            DataCadastroTextBox.Text = cliente.DataCadastro.ToString();
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
                                  Nome = NomeTextBox.Text,
                                  Codigo = GetNextCode(),
                                  Telefone = PhoneTextBox.Text,
                                  DataCadastro = Convert.ToDateTime(DataCadastroTextBox.Text)
                              };

            cliente.Endereco.Bairro = txtBairro.Text;
            cliente.Endereco.Cidade = cboCidade.SelectedIndex;
            cliente.Endereco.Logradouro = txtLogradouro.Text;
            cliente.Endereco.Numero = numberTxt.Text;

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
