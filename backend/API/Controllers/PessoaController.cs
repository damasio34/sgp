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

        [HttpGet]
        [Route("")]
        public PessoaDto GetPessoa()
        {
            return _pessoaAppService.Selecionar();
        }

        [HttpPut]
        [Route("")]
        public PessoaDto AlterarPessoa([FromBody] PessoaDto pessoaDto)
        {
            return _pessoaAppService.Alterar(pessoaDto);
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
