using System;
using System.Linq;
using Damasio34.SGP.Dominio.ModuloPessoa;
using Damasio34.SGP.Dominio.ModuloPessoa.Factories;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Damasio34.SGP.Testes
{
    [TestClass]
    public class CicloTest
    {
        private Pessoa _pessoa;

        [TestInitialize]
        public void GeraPessoa()
        {
            this._pessoa = PessoaFactory.Criar("José da Silva", "93542127604", "jose@gmail.com");
        }

        [TestMethod]
        public void CalcularBancoDeHorasComAlmoco()
        {
            var trabalho = TrabalhoFactory.Criar(_pessoa);
            trabalho.AdicionarPonto(TipoDoEvento.Entrada, DateTime.Now.AddHours(-9));
            trabalho.AdicionarPonto(TipoDoEvento.EntradaDoAlmoco, DateTime.Now.AddHours(-5));
            trabalho.AdicionarPonto(TipoDoEvento.SaidaDoAlmoco, DateTime.Now.AddHours(-4));
            trabalho.AdicionarPonto(TipoDoEvento.Saida, DateTime.Now);
            var totalDeHoras = Math.Round(trabalho.SaldoBancoHoras().TotalHours);
            Assert.AreEqual(totalDeHoras, 8);
        }
        [TestMethod]
        public void CalcularBancoDeHorasSemAlmoco()
        {
            var trabalho = TrabalhoFactory.Criar(_pessoa);
            trabalho.ControlaAlmoco = false;
            trabalho.AdicionarPonto(TipoDoEvento.Entrada, DateTime.Now.AddHours(-9));
            trabalho.AdicionarPonto(TipoDoEvento.Saida, DateTime.Now);
            var totalDeHoras = Math.Round(trabalho.SaldoBancoHoras().TotalHours);
            Assert.AreEqual(totalDeHoras, 8);
        }
    }
}
