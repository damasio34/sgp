﻿using System;
using System.Collections.Generic;
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

        private TipoDoEvento IdentificarProximoEvento(Trabalho trabalho)
        {
            var ultimoPonto = trabalho.PontosDoDia.LastOrDefault();
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

                default: return TipoDoEvento.Entrada;
            }
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

                return this.GetPontosDoDia(idTrabalho);
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
        }
        public IEnumerable<Ponto> GetPontos(Guid idTrabalho)
        {
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                var pontos = trabalho.Pontos;
                return pontos;
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
                return _usuarioRepository.Selecionar(p => p.Login.Equals(login)).Trabalhos.First(p => p.Padrao).Id;
            }
            catch (Exception ex)
            {                
                throw ex;
            }            
        }

        public ContraCheque CalcularContraCheque(Guid idTrabalho)
        {
            return this.CalcularContraCheque(idTrabalho, DateTime.Today.Month - 1);
        }

        public ContraCheque CalcularContraCheque(Guid idTrabalho, int mes)
        {
            try
            {
                var trabalho = _trabalhoRepository.Selecionar(p => p.Id.Equals(idTrabalho));
                var contracheque = trabalho.ContraCheques.FirstOrDefault(p => p.DataDeReferencia.Month.Equals(mes));            
                if (contracheque.IsNotNull()) return contracheque;

                return trabalho.GerarContraCheque(mes);
            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }
    }
}
