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
            SetEvents();

            if (pFormMode == FormType.FormMode.EditMode)
                FillGrid();
        }

        private void FillGrid()
        {
            
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
            NomeTextBox.Text = cliente.Name;
            DataCadastroTextBox.Text = cliente.CreateDate.ToString();
        }

        void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        void okButton_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente
                              {
                                  Name = NomeTextBox.Text,
                                  Code = GetNextCode(),
                                  CreateDate = Convert.ToDateTime(DataCadastroTextBox.Text)
                              };

            _clientService.Salvar(cliente);

            codeTextBox.Text = cliente.Code.ToString();
        }

        private int GetNextCode()
        {
            var quantTodosClientes = _todosClientes.Obtertodos().Count;
            return quantTodosClientes == 0 ? 1: quantTodosClientes + 1;
        }
    }
}
