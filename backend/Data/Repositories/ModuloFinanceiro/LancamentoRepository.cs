using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloFinanceiro;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Interfaces;

namespace Damasio34.SGP.Data.Repositories.ModuloFinanceiro
{
    public class LancamentoRepository :  SgpRepository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(IUnitOfWork uow) : base(uow) { }
    }
}