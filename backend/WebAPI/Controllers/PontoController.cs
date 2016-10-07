using System.Web.Http;
using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.WebAPI.Controllers
{
    public class PontoController : ApiController
    {
        public PontoController(IUnitOfWork unitOfWork)
        {
                        
        }

        [HttpPost]
        public void Post(Ponto ponto)
        {
            
        }
    }
}
