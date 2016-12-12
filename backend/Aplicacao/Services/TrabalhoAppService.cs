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
            var ultimoPonto = trabalho.PontosDoDia().OrderBy(p => p.DataHora).LastOrDefault();
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
        private ConfiguracoesDto TrabalhoToConfiguracaoDto(Trabalho trabalho)
        {
            var configuracaoDto = new ConfiguracoesDto
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
        private Trabalho ConfiguracaoDtoToTrabalho(ConfiguracoesDto configuracoesDto, Trabalho trabalho)
        {
            trabalho.HorarioDeEntrada = configuracoesDto.HorarioDeEntrada;
            trabalho.HorarioDeSaida = configuracoesDto.HorarioDeSaida;
            trabalho.ControlaAlmoco = configuracoesDto.ControlaAlmoco;
            trabalho.SalarioBruto = configuracoesDto.SalarioBruto;
            trabalho.MesesDoCiclo = configuracoesDto.MesesDoCiclo;
            trabalho.HorarioDeEntradaDoAlmoco = configuracoesDto.HorarioDeEntradaDoAlmoco;
            trabalho.HorarioDeSaidaDoAlmoco = configuracoesDto.HorarioDeSaidaDoAlmoco;

            return trabalho;
        }
        private LancamentoDoContraChequeDto LancamentoToLancamentoDto(LancamentoDoContraCheque lancamentoDoContraCheque)
        {
            var lancamentoDoContraChequeDto = new LancamentoDoContraChequeDto
            {
                Descricao = lancamentoDoContraCheque.Descricao,
                TipoDeLancamento = lancamentoDoContraCheque.TipoDeLancamento,
                Valor = lancamentoDoContraCheque.Valor
            };

            return lancamentoDoContraChequeDto;
        }
        private ContraChequeDto ContraChequeToContraChequeDto(ContraCheque contraCheque)
        {
            var contraChequeDto = new ContraChequeDto
            {
                ValorLiquido = contraCheque.ValorLiquido,
                ValorBruto = contraCheque.ValorBruto,
                LancamtentosDoContraChequeDto = contraCheque.Lancamentos.Select(LancamentoToLancamentoDto),
                DataFinalizacao = contraCheque.DataFinalizacao,
                MesDeReferencia = contraCheque.Ciclo.DateDeTermino.ToString("MMMM")
            };

            return contraChequeDto;
        }

        public PontoDto GetPonto(Guid idPonto)
        {
            try
            {
                var pontos = _trabalho.Ciclos.SelectMany(p => p.Pontos);
                var ponto = pontos.Single(p => p.Id.Equals(idPonto));
                var pontoDto = new PontoDto
                {
                    Id = ponto.Id,
                    DataHora = ponto.DataHora,
                    TipoDoEvento = ponto.TipoDoEvento,
                    Justificativa = ponto.Justificativa
                };

                return pontoDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PontosDoDiaDto MarcarPonto(DateTime? dataDeReferencia)
        {        
            try
            {
                var tipoDoEvento = IdentificarProximoEvento(_trabalho);
                _trabalho.AdicionarPonto(tipoDoEvento, dataDeReferencia);
                _trabalhoRepository.Alterar(_trabalho);
                _trabalhoRepository.Commit();

                var pontosDoDia = _trabalho.PontosDoDia();
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
                var ciclos = _trabalho.Ciclos;
                var pontos = ciclos.SelectMany(p => p.Pontos);
                var pontosDto = pontos.Select(p => new PontoDto
                {
                    Id = p.Id, DataHora = p.DataHora, TipoDoEvento = p.TipoDoEvento, Justificativa = p.Justificativa
                }).OrderByDescending(p => p.DataHora);

                return pontosDto;
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
                var pontosDoDia = _trabalho.PontosDoDia();
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
        public ContraChequeDto CalcularContraCheque()
        {
            return this.CalcularContraCheque(DateTime.Today.AddMonths(-1));
        }
        public ContraChequeDto CalcularContraCheque(DateTime dataDeReferencia)
        {
            try
            {
                var contracheque = _trabalho.GerarContraCheque(dataDeReferencia);
                var contraChequeDto = ContraChequeToContraChequeDto(contracheque);
                return contraChequeDto;
            }
            catch (Exception ex) { throw ex; }
        }
        public ConfiguracoesDto SelecionarConfiguracoes()
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
        public ConfiguracoesDto AlterarConfiguracoes(ConfiguracoesDto configuracoesDto)
        {
            try
            {
                var trabalho = ConfiguracaoDtoToTrabalho(configuracoesDto, _trabalho);
                _trabalhoRepository.Alterar(trabalho);
                _trabalhoRepository.Commit();

                return configuracoesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AlterarPonto(Guid idPonto, PontoDto pontoDto)
        {
            try
            {
                var pontosDoDia = _trabalho.PontosDoDia(pontoDto.DataHora);
                if (pontosDoDia.Any(p => p.TipoDoEvento.Equals(pontoDto.TipoDoEvento) && !p.Id.Equals(idPonto)))
                throw new Exception("Não é possível marcar dois pontos do mesmo tipo no mesmo dia.");

                var ponto = _trabalho.Pontos().Single(p => p.Id.Equals(idPonto));
                ponto.DataHora = pontoDto.DataHora;
                ponto.TipoDoEvento = pontoDto.TipoDoEvento;

                _trabalhoRepository.Alterar(_trabalho);
                // GAMB
                _trabalhoRepository.UnitOfWork.RegisterDirty(ponto);
                _trabalhoRepository.Commit();                
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
                // GAMB
                _trabalhoRepository.UnitOfWork.RegisterDeleted(ponto);
                _trabalhoRepository.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
