using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Extensions;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Dominio.ModuloPessoa;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;

namespace Damasio34.SGP.Aplicacao.Services
{
    public class TrabalhoAppService : ITrabalhoAppService
    {
        private readonly ITrabalhoRepository _trabalhoRepository;
        private readonly Trabalho _trabalho;

        public TrabalhoAppService(IAutenticacao autenticacao, ITrabalhoRepository trabalhoRepository)
        {
            this._trabalhoRepository = trabalhoRepository;
            if (autenticacao.IdUsuario.EhValido())
            {
                this._trabalho = _trabalhoRepository.BaseQuery
                    .Include("Ciclos.Pontos")
                    .SingleOrDefault(p => p.Pessoa.Id.Equals(autenticacao.IdUsuario));
                if (_trabalho.IsNull()) throw new Exception("Trabalho não encontrado.");
            }            
        }

        private TipoDoEvento IdentificarProximoEvento(Trabalho trabalho)
        {
            var ultimoPonto = trabalho.PontosDoDia.OrderBy(p => p.DataHora).LastOrDefault();
            if (ultimoPonto.IsNull()) return TipoDoEvento.Entrada;
            switch (ultimoPonto.TipoDoEvento)
            {
                case TipoDoEvento.Entrada:
                    return TipoDoEvento.EntradaDoAlmoco;
                case TipoDoEvento.EntradaDoAlmoco:
                    return TipoDoEvento.SaidaDoAlmoco;
                case TipoDoEvento.SaidaDoAlmoco:
                    return TipoDoEvento.Saida;
                case TipoDoEvento.Saida:
                    return TipoDoEvento.Entrada;

                default: throw new Exception("Tipo do próximo ponto não identificado.");
            }
        }
        private ConfiguracaoDto TrabalhoToConfiguracaoDto(Trabalho trabalho)
        {
            var configuracaoDto = new ConfiguracaoDto
            {
                IdTrabalho = trabalho.Id,
                HorarioDeEntrada = trabalho.HorarioDeEntrada,
                HorarioDeSaida = trabalho.HorarioDeSaida,
                ControlaAlmoco = trabalho.ControlaAlmoco,
                SalarioBruto = trabalho.SalarioBruto,
                MesesDoCiclo = trabalho.MesesDoCiclo,
                HorarioDeEntradaDoAlmoco = trabalho.HorarioDeEntradaDoAlmoco,
                HorarioDeSaidaDoAlmoco = trabalho.HorarioDeSaidaDoAlmoco
            };

            return configuracaoDto;
        }
        private Trabalho ConfiguracaoDtoToTrabalho(ConfiguracaoDto configuracaoDto, Trabalho trabalho)
        {
            trabalho.HorarioDeEntrada = configuracaoDto.HorarioDeEntrada;
            trabalho.HorarioDeSaida = configuracaoDto.HorarioDeSaida;
            trabalho.ControlaAlmoco = configuracaoDto.ControlaAlmoco;
            trabalho.SalarioBruto = configuracaoDto.SalarioBruto;
            trabalho.MesesDoCiclo = configuracaoDto.MesesDoCiclo;
            trabalho.HorarioDeEntradaDoAlmoco = configuracaoDto.HorarioDeEntradaDoAlmoco;
            trabalho.HorarioDeSaidaDoAlmoco = configuracaoDto.HorarioDeSaidaDoAlmoco;

            return trabalho;
        }

        public PontosDoDiaDto MarcarPonto()
        {        
            try
            {
                var tipoDoEvento = IdentificarProximoEvento(_trabalho);
                _trabalho.AdicionarPonto(tipoDoEvento);
                _trabalhoRepository.Alterar(_trabalho);
                _trabalhoRepository.Commit();

                var pontosDoDia = _trabalho.PontosDoDia;
                var deHoje = pontosDoDia as Ponto[] ?? pontosDoDia.ToArray();
                var configuracoesDoUsuarioDto = new PontosDoDiaDto
                {
                    IdTrabalho = _trabalho.Id,
                    ControlaAlmoco = _trabalho.ControlaAlmoco,
                    HorarioDeEntrada = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Entrada))?.DataHora,
                    HorarioDeSaida = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Saida))?.DataHora,
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
        public IEnumerable<PontoDto> GetPontos()
        {
            try
            {
                return _trabalho.Pontos().Select(p => new PontoDto
                {
                    Id = p.Id, DataHora = p.DataHora, TipoDoEvento = p.TipoDoEvento, Justificativa = p.Justificativa
                }).OrderByDescending(p => p.DataHora).ToList();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        public PontosDoDiaDto GetPontosDoDia()
        {
            try
            {
                var pontosDoDia = _trabalho.PontosDoDia;
                var deHoje = pontosDoDia as Ponto[] ?? pontosDoDia.ToArray();
                var configuracoesDoUsuarioDto = new PontosDoDiaDto
                {
                    IdTrabalho = _trabalho.Id,
                    ControlaAlmoco = _trabalho.ControlaAlmoco,
                    HorarioDeEntrada = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Entrada))?.DataHora,
                    HorarioDeSaida = deHoje.FirstOrDefault(p => p.TipoDoEvento.Equals(TipoDoEvento.Saida))?.DataHora,
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
        public ContraCheque CalcularContraCheque()
        {
            return this.CalcularContraCheque(DateTime.Today);
        }
        public ContraCheque CalcularContraCheque(DateTime dataDeReferencia)
        {
            try
            {
                var contracheque = _trabalho.GerarContraCheque(dataDeReferencia);
                return contracheque;
            }
            catch (Exception ex) { throw ex; }
        }
        public ConfiguracaoDto SelecionarConfiguracao()
        {
            try
            {
                var configuracaoDto = TrabalhoToConfiguracaoDto(_trabalho);
                return configuracaoDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ConfiguracaoDto AtualizarConfiguracao(ConfiguracaoDto configuracaoDto)
        {
            try
            {
                var trabalho = ConfiguracaoDtoToTrabalho(configuracaoDto, _trabalho);
                _trabalhoRepository.Alterar(trabalho);

                return configuracaoDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeletePonto(Guid idPonto)
        {
            try
            {
                var ponto = _trabalho.Pontos().Single(p => p.Id.Equals(idPonto));
                _trabalho.RemoverPonto(ponto);

                _trabalhoRepository.Alterar(_trabalho);
                _trabalhoRepository.Commit();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
