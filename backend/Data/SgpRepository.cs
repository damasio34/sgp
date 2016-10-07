using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Repositories;
using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.Interfaces;

namespace Damasio34.SGP.Data
{
    public class SgpRepository<TEntidade> : Repository<TEntidade>, ISgpRepository<TEntidade> where TEntidade : EntidadeBase
    {
        protected SgpRepository(IUnitOfWork uow) : base(uow) { }

        public void Commit()
        {
            UnitOfWork.Commit();
        }
    }
}