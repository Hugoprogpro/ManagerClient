using System.Collections.Generic;

namespace ManagerClient.Domain
{
    public interface ITodosClientes
    {
        Cliente Inserir(Cliente cliente);

        List<Cliente> Obtertodos();
        Cliente GetByKey(int Id);
        Cliente GetByName(string name); 
    }
}
