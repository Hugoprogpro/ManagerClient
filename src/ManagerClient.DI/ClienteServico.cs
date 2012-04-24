using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ManagerClient.Domain
{
    public class ClienteServico
    {
        public ITodosClientes _todosClientes;

        public ClienteServico(ITodosClientes todosClientes)
        {
            _todosClientes = todosClientes;
        }

        public void Salvar(Cliente cliente)
        {
            cliente.DataCadastro = DateTime.Now;
            cliente.Codigo = GetNextSequenceNumber();

            cliente.VerificarSeNomeEhVazioOuNulo();

            _todosClientes.Inserir(cliente);
        }

        public Cliente GetByKey(int Id)
        {
            return _todosClientes.ObterPor(Id);
        }

        public int GetNextSequenceNumber()
        {
            if (_todosClientes.ObterListatodos().Count == 0)
                return 1;
            
            var lastCode = _todosClientes.ObterListatodos().Last().Codigo;

            return lastCode == 0 ? 1 : lastCode + 1;
        }

        public DataTable GetByName(string clientName)
        {
            return _todosClientes.ObterPor(clientName);
        }

        public DataTable Obtertodos()
        {
            return _todosClientes.ObterTodos();
        }
    }
}
