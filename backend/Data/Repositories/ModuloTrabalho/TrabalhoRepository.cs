using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;

namespace Damasio34.SGP.Data.Repositories.ModuloTrabalho
{
    public class TrabalhoRepository : SgpRepository<Trabalho>, ITrabalhoRepository
    {
        public TrabalhoRepository(IUnitOfWork uow) : base(uow) { }
    }
}