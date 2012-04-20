using System;
using ManagerClient.Domain;
using ManagerCliente.Infra;
using NUnit.Framework;

namespace ManagerClient.Tests
{
    [TestFixture]
    public class ClienteTeste
    {
        const string connectionString = @"Server=localhost\CURSO;Database=DBManagerClient;Trusted_Connection=true;";
        public TodosClientesBanco _todosClientes { get; set; } 

        [SetUp]
        public void Setup()
        {
            _todosClientes = new TodosClientesBanco(connectionString);
        }

        [Test]
        public void Nao_Deve_Permitir_Inserir_Cliente_Com_Codigo_Repetido()
        {
            var cliente = new Cliente();
            cliente.Name = "Luiz Fernando Andrade";
            cliente.Code = 1;
            cliente.CreateDate = DateTime.Now;

            Assert.IsNotNull(_todosClientes.Inserir(cliente));

            var cliente2 = new Cliente();
            cliente2.Name = "Luiz Fernando Andrade";
            cliente2.CreateDate = DateTime.Now;

            Assert.Throws<Exception>(() => _todosClientes.Inserir(cliente2));
        }

        [Test]
        public void Deve_Retornar_Um_Codigo_De_Cliente_Sequencialmente()
        {
            var cliente = new Cliente();
            cliente.Name = "Nome Teste";
            cliente.CreateDate = DateTime.Now;

            _todosClientes.Inserir(cliente);

            var clienteSalved = _todosClientes.GetByName(cliente.Name);

            Assert.IsNotNull(clienteSalved.Code);
            Assert.AreEqual(1, clienteSalved.Code);
        }

        [Test]
        public void Deve_Retornar_Um_Cliente_Pelo_Nome()
        {
            var cliente = new Cliente();
            cliente.Name = "Henrique";
            cliente.CreateDate = DateTime.Now;

            _todosClientes.Inserir(cliente);
            var clientSalved = _todosClientes.GetByName(cliente.Name);

            Assert.IsNotNull(clientSalved);
            Assert.AreEqual("Henrique                                          ", clientSalved.Name);
        }

        [Test]
        public void Todo_Cliente_Deve_Ter_Um_Nome()
        {
            var cliente = new Cliente
                              {
                                  Name = "Cleiviane Costa"
                              };

            Assert.AreEqual("Cleiviane Costa", cliente.Name);
        }

        [Test]
        public void Nome_Do_Cliente_Nao_Pode_Ser_Nulo()
        {
            var cliente = new Cliente();
            cliente.Name = null;

            Assert.Throws<Exception>(() => cliente.VerificarSeNomeEhVazioOuNulo());
        }

        [Test]
        public void Todo_Cliente_Tem_Data_De_Cadastro()
        {
            var dataCadastro = new DateTime(2012, 4, 14);
            var cliente = new Cliente();
            cliente.CreateDate = dataCadastro;

            Assert.AreEqual(dataCadastro, cliente.CreateDate);
        }

        [Test]
        public void Posso_Armazenar_Um_Cliente()
        {
            var clienteServico = new ClienteServico(_todosClientes);

            var cliente = new Cliente
                              {
                                  Name = "Cleiviane Costa"
                              };

            clienteServico.Salvar(cliente);

            var clientes = _todosClientes.GetByKey(cliente.Code);

            Assert.NotNull(clientes);
        }
    }
}
