using ManagerClient.Domain;
using NUnit.Framework;

namespace ManagerClient.Tests
{
    [TestFixture]
    public class EnderecoTest
    {
        [Test]
        public void Deve_Existir_Endereco()
        {
            var endereco = new Endereco();
            Assert.IsNotNull(endereco);
        }

        [Test]
        public void Todo_Endereco_Deve_Ter_Um_Bairro()
        {
            var endereco = new Endereco
                               {
                                   Bairro = "Nova Esperanca"
                               };

            Assert.AreEqual("Nova Esperanca", endereco.Bairro);
        }

        [Test]
        public void Todo_Endereco_Tem_Um_Logradouro()
        {
            var endereco = new Endereco
                               {
                                   Logradouro = "Nova Esperanca"
                               };
            Assert.AreEqual("Nova Esperanca", endereco.Logradouro);
        }
    }
}