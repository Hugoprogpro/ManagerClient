using System;

namespace ManagerClient.Domain
{
    public class ClienteServico
    {
        private readonly ITodosClientes _todosClientes;

        public ClienteServico(ITodosClientes todosClientes)
        {
            _todosClientes = todosClientes;
        }

        public void Salvar(Cliente cliente)
        {
            cliente.CreateDate = DateTime.Now; 

            cliente.VerificarSeNomeEhVazioOuNulo();

            _todosClientes.Inserir(cliente);
        }

        public Cliente GetByKey(int Id)
        {
            return _todosClientes.GetByKey(Id);
        }
    }
}
