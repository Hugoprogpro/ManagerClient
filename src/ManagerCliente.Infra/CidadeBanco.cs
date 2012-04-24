using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ManagerClient.Domain;

namespace ManagerCliente.Infra
{
    public class CidadeBanco : ICidades
    {
        private readonly string _connectionString =
            @"Server=localhost\CURSO;Database=DBManagerClient;Trusted_Connection=true;";

        public CidadeBanco(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection ObterConexao(string connetionString)
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public Cidade Inserir(Cidade cidade)
        {
            var query = String.Format("Insert into Cidades values('{0}')", cidade.Descricao);

            var connection = ObterConexao(_connectionString);

            var command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            return cidade;
        }

        public List<Cidade> ObterListatodos()
        {
            var connection = ObterConexao(_connectionString);
            const string query = "Select * from Cidades order by Id";

            var command = new SqlCommand(query, connection);
            var dtReader = command.ExecuteReader();
            var cidades = new List<Cidade>();

            while (dtReader.Read())
            {
                var cidade = new Cidade();

                cidade.Id = Convert.ToInt32(dtReader["Id"]);
                cidade.Descricao = dtReader["Descricao"].ToString();

                cidades.Add(cidade);
            }

            return cidades;
        }
    }
}
