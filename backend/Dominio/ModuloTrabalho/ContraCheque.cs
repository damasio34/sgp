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
            this.Ciclo = ciclo;
        }

        #endregion

        #region [ Propriedades ]

        public double ValorBruto { get; set; }
        public virtual Ciclo Ciclo { get; protected set; }
        public DateTime? DataFinalizacao { get; set; }        

        public virtual IList<LancamentoDoContracheque> Lancamentos { get; set; } = new List<LancamentoDoContracheque>();

        // Propriedades calculadas
        public double ValorLiquido => Math.Round(Ciclo.Trabalho.SalarioBruto + Lancamentos.Sum(s => s.Valor), 2);

        #endregion

        #region [ Métodos públicos ]

        public void AdiconarLancamento(Imposto imposto, TipoDeLancamento tipoDeLancamento)
        {
            var lancamento = new LancamentoDoContracheque(this, imposto.Valor, tipoDeLancamento, 
                imposto.TipoDoImposto.ToString());
            this.Lancamentos.Add(lancamento);
        }
        public void Calcular()
        {
            var inss = new Inss(this.ValorLiquido);
            this.AdiconarLancamento(inss, TipoDeLancamento.Saida);

            var irrf = new Irrf(this.ValorLiquido);
            this.AdiconarLancamento(irrf, TipoDeLancamento.Saida);
        }

        #endregion
    }
}