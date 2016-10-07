using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloFinanceiro;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Interfaces;

namespace Damasio34.SGP.Data.Repositories.ModuloFinanceiro
{
    public class ContaRepository : SgpRepository<Conta>, IContaRepository
    {
        public ContaRepository(IUnitOfWork uow) : base(uow) { }
    }
}