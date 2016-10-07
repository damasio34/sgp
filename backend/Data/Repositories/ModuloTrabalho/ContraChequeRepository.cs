using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;

namespace Damasio34.SGP.Data.Repositories.ModuloTrabalho
{
    public class ContraChequeRepository :  SgpRepository<ContraCheque>, IContraChequeRepository
    {
        public ContraChequeRepository(IUnitOfWork uow) : base(uow) { }
    }
}