using System;
using System.Collections.Generic;
using System.Web.Http;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Interfaces;
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

        [Route("{idtrabalho}/configuracao")]
        [HttpGet]
        public ConfiguracaoDto GetConfiguracao([FromUri] Guid idTrabalho)
        {
            return _trabalhoAppService.SelecionarConfiguracao(idTrabalho);
        }

        [Route("{idtrabalho}/ponto")]
        [HttpGet]
        public IEnumerable<PontoDto> GetPontos([FromUri] Guid idTrabalho)
        {
            return _trabalhoAppService.GetPontos(idTrabalho);
        }
        [Route("{idtrabalho}/ponto/{idPonto}")]
        [HttpDelete]
        public void DeletePonto([FromUri] Guid idTrabalho, [FromUri] Guid idPonto)
        {
            _trabalhoAppService.DeletePonto(idTrabalho, idPonto);
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

        [Route("{idtrabalho}/configuracao")]
        [HttpPut]
        public ConfiguracaoDto PutConfiguracao([FromUri] Guid idTrabalho, [FromBody] ConfiguracaoDto configuracaoDto)
        {
            return _trabalhoAppService.AtualizarConfiguracao(idTrabalho, configuracaoDto);
        }

        [Route("{idtrabalho}/contracheque")]
        [HttpGet]
        public ContraCheque GetContraCheque([FromUri] Guid idTrabalho)
        {
            return _trabalhoAppService.CalcularContraCheque(idTrabalho);
        }

        [Route("{idtrabalho}/contracheque/{mes}")]
        [HttpGet]
        public ContraCheque GetContraCheque([FromUri] Guid idTrabalho, [FromUri] DateTime dataDeReferencia)
        {
            return _trabalhoAppService.CalcularContraCheque(idTrabalho, dataDeReferencia);
        }
    }
}
