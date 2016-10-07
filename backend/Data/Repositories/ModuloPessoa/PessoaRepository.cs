using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloPessoa;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;

namespace Damasio34.SGP.Data.Repositories.ModuloPessoa
{
    public class PessoaRepository :  SgpRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(IUnitOfWork uow) : base(uow) { }
    }
}