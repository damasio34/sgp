using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.Interfaces;
using Damasio34.SGP.Dominio.ModuloFinanceiro;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class ContraCheque : EntidadeBase
    {
        #region [ Contrutores ]

        internal ContraCheque() { }
        internal ContraCheque(Trabalho trabalho, DateTime dataDeReferencia)
        {
            this.Trabalho = trabalho;
            this.IdTrabalho = trabalho.Id;

            this.DataDeReferencia = dataDeReferencia;            
        }

        #endregion

        #region [ Propriedades ]

        public decimal ValorBruto { get; private set; }                
        public DateTime? DataFinalizacao { get; set; }
        public DateTime DataDeReferencia { get; set; }
        public Guid IdTrabalho { get; set; }
        public virtual Trabalho Trabalho { get; protected set; }
        public virtual IList<Lancamento> Lancamentos { get; set; } = new List<Lancamento>();
        public double ValorLiquido => Math.Round(Trabalho.SalarioBruto + Lancamentos.Sum(s => s.Valor), 2);

        #endregion

        #region [ Métodos públicos ]

        public void AdiconarLancamento(Lancamento lancamento)
        {
            this.Lancamentos.Add(lancamento);
        }
        public void Calcular()
        {
            var inss = new Inss(this.ValorLiquido).Valor;
            var lancamentoDeInss = new Lancamento(inss, TipoLancamento.Saida, "INSS");
            this.AdiconarLancamento(lancamentoDeInss);
            var irrf = new Irrf(this.ValorLiquido).Valor;
            var lancamentoDeIrrf = new Lancamento(irrf, TipoLancamento.Saida, "IRRF");
            this.AdiconarLancamento(lancamentoDeIrrf);
        }

        #endregion
    }
}