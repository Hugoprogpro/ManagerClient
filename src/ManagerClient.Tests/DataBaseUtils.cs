using System.Data.SqlClient;
using ManagerCliente.Infra;

namespace ManagerClient.Tests
{
    class DataBaseUtils
    {
        private readonly string _connetionString = @"Server=localhost\CURSO;Database=DBManagerClientTest;Trusted_Connection=true;";
        
        public TodosClientesBanco _todosClientes;
        
        public DataBaseUtils(string connetionString)
        {
            _connetionString = connetionString;
            _todosClientes = new TodosClientesBanco(connetionString);
        }

        public void RemoveDadosDaTabelaCliente()
        {
            var connection = _todosClientes.ObterConexao(_connetionString);
            var query = string.Format("Delete From Cliente");

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();            
        }
    }
}
