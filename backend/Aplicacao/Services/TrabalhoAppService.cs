﻿using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Extensions;
using Damasio34.SGP.Aplicacao.Dtos;
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
        private readonly IPessoaRepository _pessoaRepository;

        public TrabalhoAppService(ITrabalhoRepository trabalhoRepository, 
            IUsuarioRepository usuarioRepository, IPessoaRepository pessoaRepository    )
        {
            this._trabalhoRepository = trabalhoRepository;
            this._usuarioRepository = usuarioRepository;
            this._pessoaRepository = pessoaRepository;
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

        public PontosDoDiaDto MarcarPonto(Guid idTrabalho)
        {        
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                if (trabalho.IsNull()) throw new Exception("Trabalho não encontrado.");
                else
                {
                    var tipoDoEvento = IdentificarProximoEvento(trabalho);
                    trabalho.AdicionarPonto(tipoDoEvento);
                }

                _trabalhoRepository.Alterar(trabalho);
                _trabalhoRepository.Commit();

                var pontosDoDia = trabalho.PontosDoDia;
                var deHoje = pontosDoDia as Ponto[] ?? pontosDoDia.ToArray();
                var configuracoesDoUsuarioDto = new PontosDoDiaDto
                {
                    IdTrabalho = trabalho.Id,
                    ControlaAlmoco = trabalho.ControlaAlmoco,
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
        public IEnumerable<PontoDto> GetPontos(Guid idTrabalho)
        {
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                return trabalho.Pontos().Select(p => new PontoDto
                {
                    Id = p.Id, DataHora = p.DataHora, TipoDoEvento = p.TipoDoEvento, Justificativa = p.Justificativa
                }).OrderByDescending(p => p.DataHora).ToList();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        public PontosDoDiaDto GetPontosDoDia(Guid idTrabalho)
        {
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                var pontosDoDia = trabalho.PontosDoDia;
                var deHoje = pontosDoDia as Ponto[] ?? pontosDoDia.ToArray();
                var configuracoesDoUsuarioDto = new PontosDoDiaDto
                {
                    IdTrabalho = trabalho.Id,
                    ControlaAlmoco = trabalho.ControlaAlmoco,
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
        public Guid GetPadrao(string login)
        {
            try
            {
                var usuario = _usuarioRepository.Selecionar(p => p.Login.Equals(login));
                var pessoa = _pessoaRepository.Selecionar(p => p.Id.Equals(usuario.IdPessoa));
                return pessoa.Trabalho.Id;
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
        }
        public ContraCheque CalcularContraCheque(Guid idTrabalho)
        {
            return this.CalcularContraCheque(idTrabalho, DateTime.Today);
        }
        public ContraCheque CalcularContraCheque(Guid idTrabalho, DateTime dataDeReferencia)
        {
            //try
            //{
            //    var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
            //    var contracheque = trabalho.ContraCheque.FirstOrDefault(p => p.DataDeReferencia.Month.Equals(mes));            
            //    if (contracheque.IsNotNull()) return contracheque;

            //    return trabalho.GerarContraCheque(mes);
            //}
            //catch (Exception ex)
            //{               
            //    throw ex;
            //}
            throw new NotImplementedException();
        }
        public ConfiguracaoDto SelecionarConfiguracao(Guid idTrabalho)
        {
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                var configuracaoDto = TrabalhoToConfiguracaoDto(trabalho);

                return configuracaoDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ConfiguracaoDto AtualizarConfiguracao(Guid idTrabalho, ConfiguracaoDto configuracaoDto)
        {
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                trabalho = ConfiguracaoDtoToTrabalho(configuracaoDto, trabalho);
                _trabalhoRepository.Alterar(trabalho);

                return configuracaoDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeletePonto(Guid idTrabalho, Guid idPonto)
        {
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                var ponto = trabalho.Pontos().Single(p => p.Id.Equals(idPonto));
                trabalho.RemoverPonto(ponto);

                _trabalhoRepository.Alterar(trabalho);
                _trabalhoRepository.Commit();
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
