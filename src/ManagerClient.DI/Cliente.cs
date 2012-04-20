using System;

namespace ManagerClient.Domain
{
    public class Cliente 
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public void VerificarSeNomeEhVazioOuNulo()
        {
            if (Name == "")
                throw new Exception("Nome não pode ser vazio!");

            if (Name == null)
                throw new Exception("Nome não pode ser nulo!");
        }
    }
}
