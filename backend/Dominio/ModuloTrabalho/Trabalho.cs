﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Extensions;
using Damasio34.SGP.Dominio.ModuloPessoa;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class Trabalho : EntidadeBase
    {
        #region [ Contrutores ]

        protected Trabalho() {}
        internal Trabalho(Pessoa pessoa, double salarioBruto, TimeSpan horarioDeEentrada, TimeSpan horarioDeSaida, 
            int mesesDoDoCliclo, TimeSpan? horarioDeEntradaDoAlmoco, TimeSpan? horarioDeSaidaDoAlmoco)
        {
            this.Pessoa = pessoa;
            this.IdPessoa = pessoa.Id;

            this.SalarioBruto = salarioBruto;

            this.HorarioDeEntrada = horarioDeEentrada;
            this.HorarioDeSaida = horarioDeSaida;
            this.HorarioDeEntradaDoAlmoco = horarioDeEntradaDoAlmoco;
            this.HorarioDeSaidaDoAlmoco = horarioDeSaidaDoAlmoco;
            this.MesesDoCiclo = mesesDoDoCliclo;

            this.ControlaAlmoco = horarioDeEntradaDoAlmoco.IsNotNull() && horarioDeSaidaDoAlmoco.IsNotNull();            
        }

        #endregion

        #region [ Propriedades ]
        
        public TimeSpan HorarioDeEntrada { get; set; }
        public TimeSpan HorarioDeSaida { get; set; }        
        public TimeSpan? HorarioDeEntradaDoAlmoco { get; set; }
        public TimeSpan? HorarioDeSaidaDoAlmoco { get; set; }

        public Guid IdPessoa { get; set; }
        public Pessoa Pessoa { get; set; }

        public int MesesDoCiclo { get; set; }
        public double SalarioBruto { get; set; }
        public bool ControlaAlmoco { get; set; }

        public virtual IList<Ciclo> Ciclos { get; protected set; } = new List<Ciclo>();        

        // Propriedades calculadas
        public int DiaDeFechamento => 1;
        public int HorasPorMes => 220;        
        public TimeSpan CargaHorariaDiaria => new TimeSpan(8, 0, 0);
        public double ValorHora => this.SalarioBruto.Equals(0) ? 0 : this.SalarioBruto / this.HorasPorMes;                       
        public IEnumerable<Ponto> PontosDoDia => this.Pontos(DateTime.Today).Where(p => p.DataHora.CompareTo(DateTime.Today) >= 0);        
        public TimeSpan TempoDeAlmoco => this.HorarioDeEntradaDoAlmoco.IsNotNull() && this.HorarioDeSaidaDoAlmoco.IsNotNull() ?
            HorarioDeSaidaDoAlmoco.Value - HorarioDeEntradaDoAlmoco.Value : new TimeSpan(1, 0, 0);

        #endregion

        #region [ Métodos Públicos ]

        private Ciclo NovoCliclo(DateTime dataDeInicio, DateTime dataDeTermino)
        {
            var ciclo = new Ciclo(dataDeInicio, dataDeTermino, this.ControlaAlmoco,
                this.CargaHorariaDiaria, this.TempoDeAlmoco);
            ciclo.GerarId();

            return ciclo;
        }

        public virtual IEnumerable<Ponto> Pontos(DateTime? dataDeReferencia = null)
        {
            var cicloAtual = BuscaAtual(dataDeReferencia);
            return cicloAtual.IsNull() ? new List<Ponto>() : cicloAtual.Pontos;
        }            
        public Ciclo BuscaAtual(DateTime? dataDeReferencia = null)
        {
            var dtReferencia = dataDeReferencia.IsNull() ? DateTime.Today : dataDeReferencia.Value;
            var cicloAtual = this.Ciclos.SingleOrDefault(p => p.DataDeInicio.Date <= dtReferencia.Date && 
                p.DateDeTermino.Date >= dtReferencia.Date);

            return cicloAtual;
        }
        public TimeSpan SaldoBancoHoras(DateTime? dataDeReferencia = null)
        {
            var cicloAtual = BuscaAtual(dataDeReferencia);
            return cicloAtual.IsNull() ? new TimeSpan(0, 0, 0) : cicloAtual.SaldoDeHoras;
        }
        public TimeSpan SaldoBancoHorasExtras(DateTime? dataDeReferencia = null)
        {
            var cicloAtual = BuscaAtual(dataDeReferencia);
            return cicloAtual.IsNull() ? new TimeSpan(0, 0, 0) : cicloAtual.SaldoDeHorasExtras;
        }
        public double ValorBancoHoras(DateTime? dataDeReferencia = null)
        {
            var horasExtrasTrabalhadas = SaldoBancoHorasExtras(dataDeReferencia).TotalHours;
            var valorHora = ValorHora > 0 ? Math.Round(ValorHora, 2) + 1.50 : Math.Round(ValorHora, 2); // Valor hora + 50%
            return horasExtrasTrabalhadas * valorHora;
        } 
        public void AdicionarPonto(Ponto ponto)
        {
            var cicloAtual = BuscaAtual(ponto.DataHora);
            if (cicloAtual.IsNull())
            {                
                cicloAtual = this.NovoCliclo(ponto.DataHora.Date, ponto.DataHora.Date.AddMonths(MesesDoCiclo).AddDays(-1));
                this.Ciclos.Add(cicloAtual);
            }

            cicloAtual.Pontos.Add(ponto);
        }
        public void AdicionarPonto(TipoDoEvento tipoDoEvento, DateTime? dataHora = null)
        {
            var ponto = PontoFactory.Criar(tipoDoEvento, dataHora);        
            this.AdicionarPonto(ponto);
        }
        public ContraCheque GerarContraCheque(DateTime dataDeReferencia)
        {
            var cicloAtual = BuscaAtual(dataDeReferencia);
            var contraCheque = ContraChequeFactory.Criar(cicloAtual);

            return contraCheque;
        }
        public void RemoverPonto(Ponto ponto)
        {
            var cicloAtual = BuscaAtual(ponto.DataHora);
            cicloAtual.Pontos.Remove(ponto);
        }

        #endregion
    }
}