using System;
using System.Collections.Generic;
using System.Web.Http;
using Damasio34.SGP.Aplicacao;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloPessoa;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/trabalho")]    
    public class TrabalhoController : ApiController
    {
        private readonly ITrabalhoAppService _trabalhoAppService;
        
        public TrabalhoController(ITrabalhoAppService trabalhoAppService)
        {
            this._trabalhoAppService = trabalhoAppService;
        }

        [Route("padrao")]
        [HttpGet]
        public Guid GetPadrao()
        {
            return _trabalhoAppService.GetPadrao(User.Identity.Name);
        }

        [Route("{idtrabalho}/ponto")]
        [HttpGet]
        public IEnumerable<Ponto> GetPontos([FromUri] Guid idTrabalho)
        {
            return _trabalhoAppService.GetPontos(idTrabalho);
        }

        [Route("{idtrabalho}/ponto/dodia")]
        [HttpGet]
        public PontosDoDiaDto GetPontosDoDia([FromUri] Guid idTrabalho)
        {
            return _trabalhoAppService.GetPontosDoDia(idTrabalho);
        }


        [HttpPost]
        [Route("{idtrabalho}/ponto/marcar")]
        public PontosDoDiaDto Post([FromUri] Guid idTrabalho)
        {
            return _trabalhoAppService.MarcarPonto(idTrabalho);
        }
    }
}
