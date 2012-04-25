using System.Data.SqlClient;
using ManagerCliente.Infra;

namespace ManagerClient.Tests
{
    class DataBaseUtils
    {
        private readonly string _connectionString =
            @"Server=CLEIVIANE-PC\TESTE;Database=Consultorio;User ID=sa;Password=sap@123;Trusted_Connection=False;";

        
        public TodosClientesBanco _todosClientes;
        
        public DataBaseUtils(string connetionString)
        {
            _connectionString = connetionString;
            _todosClientes = new TodosClientesBanco(connetionString);
        }

        public void RemoveDadosDaTabelaCliente()
        {
            var connection = _todosClientes.ObterConexao(_connectionString);
            var query = string.Format("Delete From Cliente");

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();            
        }
    }
}
