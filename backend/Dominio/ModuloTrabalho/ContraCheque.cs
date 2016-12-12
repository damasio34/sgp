using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class ContraCheque : EntidadeBase
    {
        #region [ Contrutores ]

        internal ContraCheque() { }
        internal ContraCheque(Ciclo ciclo)
        {
            ciclo.ContraCheque = this;
            this.Ciclo = ciclo;
            this.ValorBruto = Ciclo.Trabalho.SalarioBruto;
        }

        #endregion

        #region [ Propriedades ]

        public double ValorBruto { get; set; }
        public virtual Ciclo Ciclo { get; protected set; }
        public DateTime? DataFinalizacao { get; set; }

        public virtual IList<LancamentoDoContraCheque> Lancamentos { get; set; } = new List<LancamentoDoContraCheque>();

        // Propriedades calculadas
        public double ValorLiquido => Math.Round(Lancamentos.Sum(s => s.Valor), 2);

        #endregion

        #region [ Métodos públicos ]

        public void AdiconarLancamento(double valor, TipoDeLancamento tipoDeLancamento, string descricao)
        {
            var lancamento = new LancamentoDoContraCheque(this, valor, tipoDeLancamento, descricao);
            this.Lancamentos.Add(lancamento);
        }

        public void AdiconarLancamento(Imposto imposto)
        {
            var lancamento = new LancamentoDoContraCheque(this, imposto.Valor, TipoDeLancamento.Saida, 
                imposto.TipoDoImposto.ToString());
            this.Lancamentos.Add(lancamento);
        }
        public void Calcular()
        {
            this.AdiconarLancamento(ValorBruto, TipoDeLancamento.Entrada, "Salário Normal");

            var inss = new Inss(this.ValorLiquido);
            this.AdiconarLancamento(inss);

            var irrf = new Irrf(this.ValorLiquido);
            this.AdiconarLancamento(irrf);                       
        }

        #endregion
    }
}