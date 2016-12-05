using System;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Resources;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro.Factories
{
    public static class LancamentoFactory
    {
        public static Lancamento Criar(double valor, TipoLancamento tipoLancamento)
        {
            if (valor.Equals(0)) throw new ArgumentException(Mensagens.ValorLancamentoIgualZero);

            var lancamento = new Lancamento
            {
                Valor = tipoLancamento == TipoLancamento.Saida && valor > 0 ? valor*-1 : valor,
                TipoLancamento = tipoLancamento
            };
            return lancamento;
        }
    }
}