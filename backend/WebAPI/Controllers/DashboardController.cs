using System;
using System.Web.Http;
using Damasio34.SGP.Aplicacao;
using Damasio34.SGP.Aplicacao.Interfaces;

namespace Damasio34.SGP.WebAPI.Controllers
{
    [Authorize]
    public class DashboardController : ApiController
    {
        private readonly ITrabalhoAppService _trabalhoAppService;
        
        public DashboardController(ITrabalhoAppService trabalhoAppService)
        {
            this._trabalhoAppService = trabalhoAppService;
        }

        [HttpGet]
        public ConfiguracoesDoUsuarioDto Get()
        {
            var trabalho = _trabalhoAppService.GetTrabalho(User.Identity.Name);
            var configuracoesDoUsuarioDto = new ConfiguracoesDoUsuarioDto
            {
                IdTrabalho = trabalho.Id
                //HorarioDeEntrada
            };

            return configuracoesDoUsuarioDto;
        }

        [HttpPost]
        public void Post([FromUri] Guid id)
        {
            _trabalhoAppService.MarcarPonto(id);
        }
    }
}
