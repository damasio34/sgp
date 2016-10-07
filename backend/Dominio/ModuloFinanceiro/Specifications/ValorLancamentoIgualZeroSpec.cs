using Damasio34.Seedwork.Specifications;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro.Specifications
{
    public class ValorLancamentoIgualZeroSpec : Specification<Lancamento>
    {
        public override bool SatisfiedBy(Lancamento candidate)
        {
            return candidate.Valor.Equals(0);
        }
    }
}