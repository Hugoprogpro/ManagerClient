using System.Collections.Generic;

namespace ManagerClient.Domain
{
    public interface ICidades
    {
        Cidade Inserir(Cidade cidade);
        List<Cidade> ObterListatodos();
    }
}
