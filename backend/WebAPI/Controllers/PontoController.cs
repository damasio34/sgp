using System.Web.Http;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.WebAPI.Controllers
{
    public class PontoController : ApiController
    {
        private readonly ITrabalhoAppService _trabalhoAppService;

        public PontoController(ITrabalhoAppService trabalhoAppService)
        {
            this._trabalhoAppService = trabalhoAppService;
        }
    }
}
