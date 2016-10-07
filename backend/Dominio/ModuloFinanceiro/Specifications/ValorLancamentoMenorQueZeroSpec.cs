using Damasio34.Seedwork.Specifications;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro.Specifications
{
    public class ValorLancamentoMenorQueZeroSpec : Specification<Lancamento>
    {
        public override bool SatisfiedBy(Lancamento candidate)
        {
            return candidate.Valor < 0;
        }
    }
}