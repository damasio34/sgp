using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;

namespace Damasio34.SGP.Data.Repositories.ModuloTrabalho
{
    public class PontoRepository :  SgpRepository<Ponto>, IPontoRepository
    {
        public PontoRepository(IUnitOfWork uow) : base(uow) { }
    }
}
