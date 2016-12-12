using System;
using System.Collections.Generic;
using System.Web.Http;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;
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
   

        [Route("configuracoes")]
        [HttpGet]
        public ConfiguracoesDto GetConfiguracoes()
        {
            return _trabalhoAppService.SelecionarConfiguracoes();
        }

        [Route("ponto")]
        [HttpGet]
        public IEnumerable<PontoDto> GetPontos()
        {
            return _trabalhoAppService.GetPontos();
        }

        [Route("ponto/{idPonto}")]
        [HttpGet]
        public PontoDto GetPontos(Guid idPonto)
        {
            return _trabalhoAppService.GetPonto(idPonto);
        }

        [Route("ponto/{idPonto}")]
        [HttpPut]
        public void PutPonto(Guid idPonto, [FromBody] PontoDto pontoDto)
        {
            _trabalhoAppService.AlterarPonto(idPonto, pontoDto);
        }

        [Route("ponto/{idPonto}")]
        [HttpDelete]
        public void DeletePonto([FromUri] Guid idPonto)
        {
            _trabalhoAppService.DeletePonto(idPonto);
        }

        [Route("ponto/dodia")]
        [HttpPost]
        public PontosDoDiaDto GetPontosDoDia([FromBody] DateTime dataDeReferencia)
        {
            return _trabalhoAppService.GetPontosDoDia(dataDeReferencia);
        }

        [HttpPost]
        [Route("ponto/marcar")]
        public PontosDoDiaDto Post([FromBody] DateTime dataDeReferencia)
        {
            return _trabalhoAppService.MarcarPonto(dataDeReferencia);
        }

        [Route("configuracoes")]
        [HttpPut]
        public ConfiguracoesDto PutConfiguracao([FromBody] ConfiguracoesDto configuracoesDto)
        {
            return _trabalhoAppService.AlterarConfiguracoes(configuracoesDto);
        }

        [Route("contracheque")]
        [HttpGet]
        public ContraChequeDto GetContraCheque()
        {
            return _trabalhoAppService.CalcularContraCheque();
        }

        [Route("contracheque/{dataDeReferencia}")]
        [HttpGet]
        public ContraChequeDto GetContraCheque([FromUri] DateTime dataDeReferencia)
        {
            return _trabalhoAppService.CalcularContraCheque(dataDeReferencia);
        }
    }
}
