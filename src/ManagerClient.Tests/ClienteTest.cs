using System;
using ManagerClient.Domain;
using NUnit.Framework;
using TodosClientesBanco = ManagerCliente.Infra.TodosClientesBanco;

namespace ManagerClient.Tests
{
    [TestFixture]
    public class ClienteTeste
    {
        const string connectionString = @"Server=localhost\CURSO;Database=DBManagerClient;Trusted_Connection=true;";
        public ITodosClientes _todosClientes;

        [SetUp]
        public void Setup()
        {
            _todosClientes = new TodosClientesBanco(connectionString);
            var databaseService = new DataBaseUtils(connectionString);
            databaseService.RemoveDadosDaTabelaCliente();
        }

        [Test]
        public void Deve_Retornar_Um_Codigo_De_Cliente_Sequencialmente()
        {
            var cliente = new Cliente();
            cliente.Nome = "Nome Teste";
            cliente.DataCadastro = DateTime.Now;

            _todosClientes.Inserir(cliente);
        }

        [Test]
        public void Deve_Retornar_Um_Cliente_Pelo_Nome()
        {
            var cliente = new Cliente();
            cliente.Nome = "Henrique";
            cliente.DataCadastro = DateTime.Now;

            _todosClientes.Inserir(cliente);
            var clientSalved = _todosClientes.ObterPor(cliente.Nome);

            Assert.IsNotNull(clientSalved);
        }

        [Test]
        public void Todo_Cliente_Deve_Ter_Um_Nome()
        {
            var cliente = new Cliente
                              {
                                  Nome = "Cleiviane Costa"
                              };

            Assert.AreEqual("Cleiviane Costa", cliente.Nome);
        }

        [Test]
        public void Nome_Do_Cliente_Nao_Pode_Ser_Nulo()
        {
            var cliente = new Cliente();
            cliente.Nome = null;

            Assert.Throws<Exception>(() => cliente.VerificarSeNomeEhVazioOuNulo());
        }

        [Test]
        public void Todo_Cliente_Tem_Data_De_Cadastro()
        {
            var dataCadastro = new DateTime(2012, 4, 14);
            var cliente = new Cliente();
            cliente.DataCadastro = dataCadastro;

            Assert.AreEqual(dataCadastro, cliente.DataCadastro);
        }

        [Test]
        public void Posso_Armazenar_Um_Cliente()
        {
            var clienteServico = new ClienteServico(_todosClientes);

            var cliente = new Cliente
                              {
                                  Nome = "Cleiviane Costa"
                              };

            clienteServico.Salvar(cliente);

            var clientes = _todosClientes.ObterPor(cliente.Codigo);

            Assert.NotNull(clientes);
        }
    }
}
