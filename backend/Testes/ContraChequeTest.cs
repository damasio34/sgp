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
    public class ContraChequeTest
    {
        private Pessoa _pessoa;

        [TestInitialize]
        public void GeraPessoa()
        {
            this._pessoa = PessoaFactory.Criar("José da Silva", "93542127604", "jose@gmail.com");
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeMilReais()
        {
            var salarioBruto = 1000.00;
            var trabalho = _pessoa.Trabalho;
            trabalho.SalarioBruto = salarioBruto;
            var contraCheque = ContraChequeFactory.Criar(trabalho.BuscaAtual());
            contraCheque.Calcular();

             var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 920.00);
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeTresMilReais()
        {
            var salarioBruto = 3000.00;
            var trabalho = _pessoa.Trabalho;
            trabalho.SalarioBruto = salarioBruto;
            var contraCheque = ContraChequeFactory.Criar(trabalho.BuscaAtual());
            contraCheque.Calcular();

            var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 2612.55);
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeSeteMilEQuinhentosReais()
        {
            var salarioBruto = 7500.00;
            var trabalho = _pessoa.Trabalho;
            trabalho.SalarioBruto = salarioBruto;
            var contraCheque = ContraChequeFactory.Criar(trabalho.BuscaAtual());
            contraCheque.Calcular();

            var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 5892.97);
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeDezMilReais()
        {
            var salarioBruto = 10000.00;
            var trabalho = _pessoa.Trabalho;
            trabalho.SalarioBruto = salarioBruto;
            var contraCheque = ContraChequeFactory.Criar(trabalho.BuscaAtual());
            contraCheque.Calcular();

            var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 7705.47);
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeMilReaisComBancoDeHoras()
        {
            var salarioBruto = 1000.00;
            var trabalho = _pessoa.Trabalho;
            trabalho.SalarioBruto = salarioBruto;

            var dataAtual = new DateTime(2016, 11, 1);
            var dataFinal = new DateTime(2016, 11, 30);
            while (dataAtual <= dataFinal)
            {
                if (!dataAtual.DayOfWeek.Equals(DayOfWeek.Sunday) && !dataAtual.DayOfWeek.Equals(DayOfWeek.Saturday))
                {
                    trabalho.AdicionarPonto(TipoDoEvento.Entrada,
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 8, 0, 0));
                    trabalho.AdicionarPonto(TipoDoEvento.EntradaDoAlmoco,
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 12, 0, 0));
                    trabalho.AdicionarPonto(TipoDoEvento.SaidaDoAlmoco,
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 13, 0, 0));
                    trabalho.AdicionarPonto(TipoDoEvento.Saida,
                        new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 18, 0, 0));
                }
                dataAtual = dataAtual.AddDays(1);
            }

            var totalDeHorasExtras = Math.Round(trabalho.SaldoBancoHorasExtras(dataFinal).TotalHours);
            var contraCheque = ContraChequeFactory.Criar(trabalho.BuscaAtual());
            contraCheque.Calcular();

            var horasExtras = trabalho.ValorBancoHoras(dataFinal);
            var salarioLiquido = contraCheque.ValorLiquido + horasExtras;
            Assert.AreEqual(totalDeHorasExtras, 22);
            Assert.AreEqual(salarioLiquido, 1053.10);
        }
    }
}
