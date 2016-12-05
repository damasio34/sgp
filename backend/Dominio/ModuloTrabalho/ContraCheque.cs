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
        public void Finalizar()
        {
            throw new NotImplementedException();
        }

        #endregion

        public void Calcular()
        {
            var inss = new Inss(this.ValorLiquido).Valor;
            var lancamentoDeInss = new Lancamento(inss, TipoLancamento.Saida, "INSS");
            this.AdiconarLancamento(lancamentoDeInss);
            var irrf = new Irrf(this.ValorLiquido).Valor;
            var lancamentoDeIrrf = new Lancamento(irrf, TipoLancamento.Saida, "IRRF");
            this.AdiconarLancamento(lancamentoDeIrrf);
        }
    }

    public class Irrf : Imposto
    {
        public Irrf(double valor) : base(TipoDoImposto.Irrf) { this.CalcularAliquota(valor); }

        protected sealed override void CalcularAliquota(double valor)
        {            
            if (valor <= 1903.98) this.Valor = 0;
            else if (valor >= 1903.99 && valor < 2826.65) this.Valor = (0.075 * valor) - 142.80;
            else if (valor >= 2826.66 && valor < 3751.05) this.Valor = (0.15 * valor) - 354.80;
            else if (valor >= 3751.06 && valor < 4664.68) this.Valor = (0.225 * valor) - 636.13;
            else this.Valor = (0.275 * valor) - 869.36;
        }
    }

    public class Inss : Imposto
    {
        public Inss(double valor) : base(TipoDoImposto.Inss) { this.CalcularAliquota(valor); }

        protected sealed override void CalcularAliquota(double valor)
        {
            if (valor <= 1556.94) this.Valor = valor * 0.08;
            else if (valor >= 1556.95 && valor < 2594.92) this.Valor = valor * 0.09;
            else if (valor >= 1556.95 && valor < 2594.92) this.Valor = valor * 0.09;
            else if (valor >= 2594.93 && valor < 5189.82) this.Valor = valor * 0.11;
            else this.Valor = 570.88;
        }
    }

    public abstract class Imposto : IImposto
    {
        protected Imposto(TipoDoImposto tipoDoImposto)
        {
            this.TipoDoImposto = tipoDoImposto;
        }

        public TipoDoImposto TipoDoImposto { get; protected set; }
        public double Valor { get; protected set; }

        protected abstract void CalcularAliquota(double valor);
    }

    public interface IImposto
    {
        TipoDoImposto TipoDoImposto { get; }
        double Valor { get; }

    }

    public enum TipoDoImposto
    {
        Inss = 1,
        Irrf = 2
    }
}