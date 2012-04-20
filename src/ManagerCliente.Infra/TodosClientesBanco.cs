using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using ManagerClient.Domain;

namespace ManagerCliente.Infra
{
    public class TodosClientesBanco : ITodosClientes
    {
        private readonly string _connectionString;

        public TodosClientesBanco (string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection ObterConexao()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public Cliente Inserir(Cliente cliente)
        {
            cliente.Code = GetNextCode();

            var query = String.Format("Insert into Cliente values ('{0}', '{1}', '{2}')", cliente.Code, cliente.Name, cliente.CreateDate);

            var connection = ObterConexao();

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            return cliente;
        }

        private int GetNextCode()
        {
            return Obtertodos().Last().Code == 0 ? 1 : Obtertodos().Last().Code + 1;
        }

        public List<Cliente> Obtertodos()
        {
            var connection = ObterConexao();
            const string query = "Select * from cliente";

            var command = new SqlCommand(query, connection);
            var dtReader = command.ExecuteReader();
            var clientes = new List<Cliente>();

            while (dtReader.Read())
            {
                var cliente = new Cliente();
                cliente.Name = dtReader["Nome"].ToString();
                cliente.CreateDate = Convert.ToDateTime(dtReader["DataCadastro"]);
                clientes.Add(cliente);
            }

            return clientes;
        }

        public Cliente GetByKey(int code)
        {
            var connection = ObterConexao();
            string query = String.Format("Select * from Cliente where Codigo = '{0}'", code);

            var command = new SqlCommand(query, connection);
            var dtReader = command.ExecuteReader();

            var cliente = new Cliente();

            while (dtReader.Read())
            {
                cliente.Name = dtReader["Nome"].ToString();
            }

            return cliente;
        }

        public Cliente GetByName(string name) 
        {
            var connection = ObterConexao();

            var query = String.Format("Select * from Cliente Where Nome = '{0}'", name);

            var command = new SqlCommand(query, connection);
            var dtReader = command.ExecuteReader();

            var cliente = new Cliente();

            while (dtReader.Read())
            {
                cliente.Name = dtReader["Nome"].ToString();
                cliente.CreateDate = Convert.ToDateTime(dtReader["DataCadastro"]);
                cliente.Code = Convert.ToInt32(dtReader["Codigo"]);
            }

            return cliente;
        }
    }
}
