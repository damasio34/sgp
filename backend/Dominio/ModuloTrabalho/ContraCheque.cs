using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.Interfaces;
using Damasio34.SGP.Dominio.ModuloFinanceiro;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class ContraCheque : EntidadeBase, IFinalizavel
    {
        #region [ Contrutores ]

        //protected internal ContraCheque() { }
        protected internal ContraCheque(Trabalho emprego, DateTime dataReferencia)
        {
            this.Emprego = emprego;
            this.IdEmprego = emprego.Id;

            this.DataReferencia = dataReferencia;
            if (this.Lancamentos == null) this.Lancamentos = new List<Lancamento>();
        }

        #endregion

        #region [ Propriedades ]

        public decimal ValorBruto { get; private set; }
        public decimal ValorLiquido
        {
            get { return Emprego.SalarioBruto + Lancamentos.Sum(s => s.Valor); }
        }
        public DateTime? DataFinalizacao { get; set; }
        public DateTime DataReferencia { get; set; }
        public Guid IdEmprego { get; set; }
        public virtual Trabalho Emprego { get; set; }
        public virtual ICollection<Lancamento> Lancamentos { get; set; }

        #endregion

        #region [ Métodos públicos ]

        public void AdiconarLancamento(Lancamento lancamento)
        {
            this.Lancamentos.Add(lancamento);
        }
        public void Finalizar()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}