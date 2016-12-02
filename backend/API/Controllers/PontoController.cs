using System.Web.Http;
using Damasio34.SGP.Aplicacao.Interfaces;

namespace Damasio34.SGP.API.Controllers
{
    [RoutePrefix("api/ponto")]
    public class PontoController : ApiController
    {
        private readonly ITrabalhoAppService _trabalhoAppService;

        public PontoController(ITrabalhoAppService trabalhoAppService)
        {
            this._trabalhoAppService = trabalhoAppService;
        }
    }
}
