using System.Collections.Generic;
using System.Data;

namespace ManagerClient.Domain
{
    public interface ITodosClientes
    {
        Cliente Inserir(Cliente cliente);

        List<Cliente> ObterListatodos();

        DataTable ObterTodos();

        Cliente ObterPor(int Id);
        
        DataTable ObterPor(string name);
        
        bool Remove(Cliente cliente);

        bool Update(Cliente cliente);
    }
}
