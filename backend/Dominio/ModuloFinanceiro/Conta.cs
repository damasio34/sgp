using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Exceptions;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Factories;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Resources;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Specifications;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro
{
    public abstract class Conta : EntidadeBase
    {
        #region [ Contrutores ]

        protected internal Conta()
        {
            if (this.Lancamentos == null) 
                this.Lancamentos = new List<Lancamento>();

            this.AceitaSaldoNegativo = false;
        }

        #endregion

        #region [ Propriedades ]

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Lancamento> Lancamentos { get; protected set; }
        public bool AceitaSaldoNegativo { get; protected set; }
        public double Saldo => Lancamentos.Sum(s => s.Valor);        

        #endregion

        #region [ Métodos públicos ]

        public virtual void Creditar(Lancamento lancamento)
        {
            var valorLancamentoMaiorQueZeroSpec = new ValorLancamentoMaiorQueZeroSpec();
            if (!valorLancamentoMaiorQueZeroSpec.SatisfiedBy(lancamento))
                throw new SpecificationException(Mensagens.ValorLancamentoPrecisaSerMaiorQueZero);

            if (lancamento.TipoLancamento != TipoLancamento.Entrada)
                throw new DomainException(Mensagens.TipoDeLancamentoIncorreto);

            this.Lancamentos.Add(lancamento);
        }
        public virtual void Creditar(double valor)
        {
            var lancamento = LancamentoFactory.Criar(valor, TipoLancamento.Entrada);
            this.Lancamentos.Add(lancamento);
        }

        public virtual void Debitar(Lancamento lancamento)
        {
            var valorLancamentoMenorQueZeroSpec = new ValorLancamentoMenorQueZeroSpec();
            if (!valorLancamentoMenorQueZeroSpec.SatisfiedBy(lancamento))
                throw new SpecificationException(Mensagens.ValorLancamentoPrecisaSerMenorQueZero);

            if (lancamento.TipoLancamento != TipoLancamento.Saida)
                throw new DomainException(Mensagens.TipoDeLancamentoIncorreto);

            if (this.Saldo < lancamento.Valor && !this.AceitaSaldoNegativo)
                throw new SpecificationException(Mensagens.SaldoInsuficiente);
            
            this.Lancamentos.Add(lancamento);
        }
        public virtual void Debitar(double valor)
        {
            var lancamento = LancamentoFactory.Criar(valor, TipoLancamento.Saida);
            this.Lancamentos.Add(lancamento);
        }

        public virtual void Transferir(double valor, Conta contaDestino)
        {
            try
            {
                var lancamentoDebito = LancamentoFactory.Criar(valor * -1, TipoLancamento.Transferencia);

                if (this.Saldo < valor && !this.AceitaSaldoNegativo)
                    throw new SpecificationException(Mensagens.SaldoInsuficiente);
                
                this.Lancamentos.Add(lancamentoDebito);

                var lancamentoCredito = LancamentoFactory.Criar(valor, TipoLancamento.Transferencia);
                contaDestino.Lancamentos.Add(lancamentoCredito);                
            }
            catch (DomainException dex)
            {                    
                throw;
            }
        }

        #endregion
    }
}