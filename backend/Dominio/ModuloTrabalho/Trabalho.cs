using System;
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
            pessoa.Trabalho = this;

            this.Pessoa = pessoa;
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

        public Pessoa Pessoa { get; set; }

        public int MesesDoCiclo { get; set; }
        public double SalarioBruto { get; set; }
        public bool ControlaAlmoco { get; set; }

        public virtual IList<Ciclo> Ciclos { get; protected set; } = new List<Ciclo>();        

        // Propriedades calculadas
        //public int DiaDeFechamento => 24;
        public int HorasPorMes => 220;        
        public TimeSpan CargaHorariaDiaria => new TimeSpan(8, 0, 0);
        public double ValorHora => this.SalarioBruto.Equals(0) ? 0 : this.SalarioBruto / this.HorasPorMes;                       
        //public IEnumerable<Ponto> PontosDoDia => this.Pontos().Where(p => p.DataHora.Date.CompareTo(DateTime.Today) >= 0);        
        public TimeSpan TempoDeAlmoco => this.HorarioDeEntradaDoAlmoco.IsNotNull() && this.HorarioDeSaidaDoAlmoco.IsNotNull() ?
            HorarioDeSaidaDoAlmoco.Value - HorarioDeEntradaDoAlmoco.Value : new TimeSpan(1, 0, 0);

        #endregion

        #region [ Métodos Públicos ]

        private Ciclo NovoCiclo(DateTime dataDeInicio, DateTime dataDeTermino)
        {
            var ciclo = new Ciclo(this, dataDeInicio, dataDeTermino, this.ControlaAlmoco,
                this.CargaHorariaDiaria, this.TempoDeAlmoco);
            ciclo.GerarId();
            this.Ciclos.Add(ciclo);

            return ciclo;
        }

        public IEnumerable<Ponto> PontosDoDia(DateTime? dataDeReferencia = null)
        {
            return this.Pontos(dataDeReferencia).Where(p => p.DataHora.Date.CompareTo(DateTime.Today) >= 0);
        } 

        public virtual IEnumerable<Ponto> Pontos(DateTime? dataDeReferencia = null)
        {
            var cicloAtual = BuscaAtual(dataDeReferencia);
            var pontos = cicloAtual.IsNull() ? new List<Ponto>() : cicloAtual.Pontos;
            return pontos;
        }            
        public Ciclo BuscaAtual(DateTime? dataDeReferencia = null)
        {
            var dtReferencia = dataDeReferencia.IsNull() ? DateTime.Today : dataDeReferencia.Value;
            var cicloAtual = this.Ciclos.SingleOrDefault(p => p.DataDeInicio.Date <= dtReferencia.Date && 
                p.DateDeTermino.Date >= dtReferencia.Date);

            if (cicloAtual.IsNull())
            {
                var utimoDiaDoMes = DateTime.DaysInMonth(dtReferencia.Year, dtReferencia.Month);
                var dataFinal = new DateTime(dtReferencia.Year, dtReferencia.Month, utimoDiaDoMes);
                var dataInicial = new DateTime(dtReferencia.Year, dtReferencia.Month, 1);
                var novoCiclo = this.NovoCiclo(dataInicial, dataFinal);
                cicloAtual = novoCiclo;
            }

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
                cicloAtual = this.NovoCiclo(ponto.DataHora.Date, ponto.DataHora.Date.AddMonths(MesesDoCiclo).AddDays(-1));            

            cicloAtual.Pontos.Add(ponto);
        }
        public void AdicionarPonto(TipoDoEvento tipoDoEvento, DateTime? dataHora = null)
        {
            var ponto = PontoFactory.Criar(tipoDoEvento, dataHora);        
            this.AdicionarPonto(ponto);
        }
        public ContraCheque GerarContraCheque(DateTime? dataDeReferencia = null)
        {
            var cicloAtual = BuscaAtual(dataDeReferencia);
            return cicloAtual.GerarContraCheque();
        }
        public void RemoverPonto(Ponto ponto)
        {
            var cicloAtual = BuscaAtual(ponto.DataHora);
            cicloAtual.Pontos.Remove(ponto);
        }

        #endregion
    }
}