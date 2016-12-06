using System.Net;
using System.Net.Http;
using System.Web.Http;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Aplicacao.Services;

namespace Damasio34.SGP.API.Controllers
{
    [RoutePrefix("api/pessoa")]
    public class PessoaController : ApiController
    {
        private readonly IPessoaAppService _pessoaAppService;

        public PessoaController(PessoaAppService pessoaAppService)
        {
            this._pessoaAppService = pessoaAppService;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Singin([FromBody] PessoaDto pessoaDto)
        {
            _pessoaAppService.Incluir(pessoaDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }        
    }
}
