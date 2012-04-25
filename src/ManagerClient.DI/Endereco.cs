namespace ManagerClient.Domain
{
    public class Endereco
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public int Cidade { get; set; }
    }
}

