using System;
using System.Linq;
using System.Web.Http;
using Damasio34.SGP.Aplicacao;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.API.Controllers
{
    [RoutePrefix("api/dashboard")]
    [Authorize]
    public class DashboardController : ApiController
    {
        private readonly ITrabalhoAppService _trabalhoAppService;
        
        public DashboardController(ITrabalhoAppService trabalhoAppService)
        {
            this._trabalhoAppService = trabalhoAppService;
        }

        [Route("")]
        [HttpGet]
        public ConfiguracoesDoUsuarioDto Get()
        {
            var trabalho = _trabalhoAppService.GetTrabalho(User.Identity.Name);
            var pontos = trabalho.Pontos.Where(p => p.DataHora.CompareTo(DateTime.Today) >= 0);
            var deHoje = pontos as Ponto[] ?? pontos.ToArray();
            var configuracoesDoUsuarioDto = new ConfiguracoesDoUsuarioDto
            {
                IdTrabalho = trabalho.Id,
                HorarioDeEntrada = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Entrada))?.DataHora,
                HorarioDeSaida = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Saida))?.DataHora
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
