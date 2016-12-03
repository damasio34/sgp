using System;
using System.Linq;
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

        public PontosDoDiaDto MarcarPonto(Guid idTrabalho)
        {
            var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));

            try
            {
                if (trabalho.IsNull()) throw new Exception("Trabalho não encontrado.");
                else trabalho.AdicionarPonto();

                _trabalhoRepository.Alterar(trabalho);
                _trabalhoRepository.Commit();

                return this.GetPontosDoDia(idTrabalho);
            }
            catch (Exception)
            {
                
                throw;
            }            
        }
        public PontosDoDiaDto GetPontosDoDia(Guid idTrabalho)
        {
            try
            {                
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));

                var pontos = trabalho.Pontos.Where(p => p.DataHora.CompareTo(DateTime.Today) >= 0);
                var deHoje = pontos as Ponto[] ?? pontos.ToArray();
                var configuracoesDoUsuarioDto = new PontosDoDiaDto
                {
                    IdTrabalho = trabalho.Id,
                    HorarioDeEntrada = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Entrada))?.DataHora,
                    HorarioDeSaida = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Entrada))?.DataHora,
                    HorarioDeEntradaDoAlmoco = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.EntradaDoAlmoco))?.DataHora,
                    HorarioDeSaidaDoAlmoco = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.SaidaDoAlmoco))?.DataHora
                };

                return configuracoesDoUsuarioDto;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }  
        public Guid GetPadrao(string login)
        {
            try
            {
                return _usuarioRepository.Selecionar(p => p.Login.Equals(login)).Trabalhos.First(p => p.Padrao).Id;
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
        }
    }
}
