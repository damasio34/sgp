using System;
using Damasio34.Seedwork.Extensions;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;

namespace Damasio34.SGP.Aplicacao.Services
{
    public class TrabalhoAppService : ITrabalhoAppService
    {
        private readonly ITrabalhoRepository _trabalhoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TrabalhoAppService(ITrabalhoRepository trabalhoRepository, 
            IUsuarioRepository usuarioRepository)
        {
            this._trabalhoRepository = trabalhoRepository;
            this._usuarioRepository = usuarioRepository;
        }
        public void MarcarPonto(Guid idTrabalho)
        {
            var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));

            try
            {
                if (trabalho.IsNull()) return;
                else trabalho.AdicionarPonto();

                _trabalhoRepository.Alterar(trabalho);
                _trabalhoRepository.Commit();
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        public Trabalho GetTrabalho(string usuario)
        {
            var idUsuario = _usuarioRepository.Selecionar(p => p.Login.Equals(usuario)).Id;
            var trabalho = _trabalhoRepository.Selecionar(p => p.IdUsuario.Equals(idUsuario));
            return trabalho;            
        }
    }
}
