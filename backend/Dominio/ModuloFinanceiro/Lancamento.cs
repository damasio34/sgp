using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro
{
    public class Lancamento : EntidadeBase
    {
        protected internal Lancamento() { }

        public decimal Valor { get; set; }
        public TipoLancamento TipoLancamento { get; set; }
    }
}