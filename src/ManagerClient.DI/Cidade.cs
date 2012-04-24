using System;

namespace ManagerClient.Domain
{
    public class Cidade
    {
        public int Id { get; set; } 
        public string Descricao { get; set; }

        public void VerificarSeDecricaoEhVaziaOuNula()
        {
            if (Descricao == "")
                throw new Exception("Nome não pode ser vazio!");

            if (Descricao == null)
                throw new Exception("Nome não pode ser nulo!");
        }
    }
}
