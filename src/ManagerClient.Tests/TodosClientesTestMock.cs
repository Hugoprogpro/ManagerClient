using System.Collections.Generic;
using ManagerClient.Domain;
using ManagerCliente.Infra;
using NUnit.Framework;

namespace ManagerClient.Tests
{
    public class TodosClientesTestMock
    {
        readonly List<Cliente> clientes = new List<Cliente>();

        public Cliente Inserir(Cliente cliente)
        {
            //cliente.Codigo = clientes.Count == 0 ? 1 : clientes.Max(c => c.Codigo);
            cliente.Codigo = clientes.Count + 1;
            clientes.Add(cliente);

            return cliente;
        }

        public List<Cliente> Obtertodos()
        {
            return clientes;
        }
    }
}
