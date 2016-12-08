using System;
using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class LancamentoDoContraCheque : EntidadeBase
    {
        internal LancamentoDoContraCheque() { }
        internal LancamentoDoContraCheque(ContraCheque contraCheque, double valor, 
            TipoDeLancamento tipoDeLancamento, string descricao)
        {
            this.ContraCheque = contraCheque;
            this.IdContraCheque = contraCheque.Id;
            this.TipoDeLancamento = tipoDeLancamento;
            this.Descricao = descricao;

            if (this.TipoDeLancamento.Equals(TipoDeLancamento.Saida) && valor > 0) this.Valor = valor * -1;
            else this.Valor = valor;
        }

        public ContraCheque ContraCheque { get; protected set; }
        public Guid IdContraCheque { get; protected set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public TipoDeLancamento TipoDeLancamento { get; set; }        
    }
}