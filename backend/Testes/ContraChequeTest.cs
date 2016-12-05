using System;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Damasio34.SGP.Testes
{
    [TestClass]
    public class ContraChequeTest
    {
        [TestMethod]
        public void CalcularSalarioLiquidoDeMilReais()
        {
            var salarioBruto = 1000.00;
            var trabalho = TrabalhoFactory.Criar(salarioBruto);
            var contraCheque = ContraChequeFactory.Criar(trabalho, DateTime.Now.AddMonths(-1));
            contraCheque.Calcular();

             var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 920.00);
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeTresMilReais()
        {
            var salarioBruto = 3000.00;
            var trabalho = TrabalhoFactory.Criar(salarioBruto);
            var contraCheque = ContraChequeFactory.Criar(trabalho, DateTime.Now.AddMonths(-1));
            contraCheque.Calcular();

            var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 2612.55);
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeSeteMilEQuinhentosReais()
        {
            var salarioBruto = 7500.00;
            var trabalho = TrabalhoFactory.Criar(salarioBruto);
            var contraCheque = ContraChequeFactory.Criar(trabalho, DateTime.Now.AddMonths(-1));
            contraCheque.Calcular();

            var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 5892.97);
        }

        [TestMethod]
        public void CalcularSalarioLiquidoDeDezMilReais()
        {
            var salarioBruto = 10000.00;
            var trabalho = TrabalhoFactory.Criar(salarioBruto);
            var contraCheque = ContraChequeFactory.Criar(trabalho, DateTime.Now.AddMonths(-1));
            contraCheque.Calcular();

            var salarioLiquido = contraCheque.ValorLiquido;
            Assert.AreEqual(salarioLiquido, 7705.47);
        }
    }
}
