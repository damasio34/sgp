using System;
using System.Linq;
using System.Web.Http;
using Damasio34.SGP.Aplicacao;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.API.Controllers
{
    [RoutePrefix("api/trabalho")]
    [Authorize]
    public class TrabalhoController : ApiController
    {
        private readonly ITrabalhoAppService _trabalhoAppService;
        
        public TrabalhoController(ITrabalhoAppService trabalhoAppService)
        {
            this._trabalhoAppService = trabalhoAppService;
        }

        [Route("ponto/dodia")]
        [HttpGet]
        public PontosDoDiaDto Get()
        {
            return _trabalhoAppService.GetPontosDoDia(User.Identity.Name);
        }

        [HttpPost]
        [Route("ponto/marcar")]
        public void Post([FromUri] Guid id)
        {
            _trabalhoAppService.MarcarPonto(id);
        }
    }
}
