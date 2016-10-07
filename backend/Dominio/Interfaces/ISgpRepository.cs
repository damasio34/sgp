using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Repositories;

namespace Damasio34.SGP.Dominio.Interfaces
{
    public interface ISgpRepository<TEntidade> : IRepository<TEntidade> where TEntidade : EntidadeBase
    {        
        void Commit();
    }
}
