using System;

namespace ManagerClient.Domain
{
    public class Cliente 
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public Cliente()
        {
            Endereco = new Endereco();
        }

        public void VerificarSeNomeEhVazioOuNulo()
        {
            if (Nome == "")
                throw new Exception("Nome não pode ser vazio!");

            if (Nome == null)
                throw new Exception("Nome não pode ser nulo!");
        }
    }
}
