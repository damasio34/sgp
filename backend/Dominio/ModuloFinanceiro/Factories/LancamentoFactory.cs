using System;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Resources;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro.Factories
{
    public static class LancamentoFactory
    {
        public static Lancamento Criar(decimal valor, TipoLancamento tipoLancamento)
        {
            if (valor.Equals(0)) throw new ArgumentException(Mensagens.ValorLancamentoIgualZero);            
            
            var lancamento = new Lancamento();
            lancamento.Valor = tipoLancamento == TipoLancamento.Saida && valor > 0 ? decimal.Negate(valor) : valor;
            lancamento.TipoLancamento = tipoLancamento;
            return lancamento;
        }
    }
}