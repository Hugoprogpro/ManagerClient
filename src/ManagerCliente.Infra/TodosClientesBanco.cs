using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ManagerClient.Domain;

namespace ManagerCliente.Infra
{
    public class TodosClientesBanco : ITodosClientes
    {
        private readonly string _connectionString =
            @"Server=CLEIVIANE-PC\TESTE;Database=DBManagerClient;User ID=sa;Password=sap@123;Trusted_Connection=False;";

        public TodosClientesBanco (string connectionString)
        {
            _connectionString = connectionString;
        }

        public  SqlConnection ObterConexao(string connetionString)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public Cliente Inserir(Cliente cliente)
        {
            var query = String.Format("Insert into Cliente values ('{0}', '{1}', '{2}','{3}', '{4}', '{5}', '{6}', '{7}')", cliente.Codigo, cliente.Nome, 
                cliente.DataCadastro, cliente.Telefone, cliente.Endereco.Logradouro, cliente.Endereco.Bairro, cliente.Endereco.Numero,
                cliente.Endereco.Cidade);

            var connection = ObterConexao(_connectionString);

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            return cliente;
        }

        public List<Cliente> ObterListatodos()
        {
            var connection = ObterConexao(_connectionString);
            const string query = "Select * from cliente order by codigo";

            var command = new SqlCommand(query, connection);
            var dtReader = command.ExecuteReader();
            var clientes = new List<Cliente>();

            while (dtReader.Read())
            {
                var cliente = new Cliente();

                cliente.Codigo = Convert.ToInt32(dtReader["Codigo"]);
                cliente.Nome = dtReader["Nome"].ToString();
                cliente.DataCadastro = Convert.ToDateTime(dtReader["DataCadastro"]);
                cliente.Endereco.Logradouro = dtReader["Logradouro"].ToString();
                cliente.Endereco.Bairro = dtReader["Bairro"].ToString();
                cliente.Telefone = dtReader["Telefone"].ToString();

                clientes.Add(cliente);
            }

            return clientes;
        }

        public DataTable ObterTodos()
        {
            var connection = ObterConexao(_connectionString);

            var query = ("Select * from Cliente order by codigo");

            var command = new SqlCommand(query, connection);
            var dtClientes = new DataTable();

            var reader = command.ExecuteReader();

            dtClientes.Load(reader);
            return dtClientes;
        }

        public Cliente ObterPor(int codigo)
        {
            var connection = ObterConexao(_connectionString);
            string query = String.Format("Select * from Cliente where Codigo = '{0}'", codigo);

            var command = new SqlCommand(query, connection);
            var dtReader = command.ExecuteReader();

            if (dtReader.HasRows)
            {
                var cliente = new Cliente();

                while (dtReader.Read())
                {
                    cliente.Codigo = Convert.ToInt32(dtReader["Codigo"]);
                    cliente.Nome = dtReader["Nome"].ToString();
                    cliente.DataCadastro = Convert.ToDateTime(dtReader["DataCadastro"]);
                    cliente.Endereco.Logradouro = dtReader["Logradouro"].ToString();
                    cliente.Endereco.Bairro = dtReader["Bairro"].ToString();
                    cliente.Telefone = dtReader["Telefone"].ToString();
                }

                return cliente;
            }
            return null;
        }

        public DataTable ObterPor(string nome) 
        {
            var connection = ObterConexao(_connectionString);

            var query = String.Format("Select * from Cliente Where Nome Like '{0}%'", nome);

            var command = new SqlCommand(query, connection);
            var daClientes = new SqlDataAdapter(command);

            var dtClientes = new DataTable();
            daClientes.Fill(dtClientes);

            return dtClientes;
        }

        public bool Remove(Cliente cliente)
        {
            var connection = ObterConexao(_connectionString);

            var query = String.Format("Delete from Cliente Where Codigo = {0}", cliente.Codigo);

            var command = new SqlCommand(query, connection);
            
            try
            {
                command.ExecuteReader();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
