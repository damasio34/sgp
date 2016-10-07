using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloFinanceiro;
using Damasio34.SGP.Dominio.ModuloFinanceiro.Interfaces;

namespace Damasio34.SGP.Data.Repositories.ModuloFinanceiro
{
    public class CategoriaRepository : SgpRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IUnitOfWork uow) : base(uow) { }
    }
}