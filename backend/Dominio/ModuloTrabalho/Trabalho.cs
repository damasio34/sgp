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
        internal Trabalho(Pessoa pessoa, double salario, TimeSpan entrada, TimeSpan saida, int mesesDoDoCliclo,
            TimeSpan? entradaDoAlmoco, TimeSpan? saidaDoAlmoco)
        {
            this.Pessoa = pessoa;
            this.IdPessoa = pessoa.Id;

            this.SalarioBruto = salario;
            this.HorarioDeEntrada = entrada;
            this.HorarioDeSaida = saida;
            this.HorarioDeEntradaDoAlmoco = entradaDoAlmoco;
            this.HorarioDeSaidaDoAlmoco = saidaDoAlmoco;
            this.MesesDoCiclo = mesesDoDoCliclo;

            this.ControlaAlmoco = entradaDoAlmoco.IsNotNull() && saidaDoAlmoco.IsNotNull();
            this.Padrao = true;
        }

        #endregion

        #region [ Propriedades ]

        public int MesesDoCiclo { get; set; }
        public TimeSpan HorarioDeEntrada { get; set; }
        public TimeSpan HorarioDeSaida { get; set; }        
        public TimeSpan? HorarioDeEntradaDoAlmoco { get; set; }
        public TimeSpan? HorarioDeSaidaDoAlmoco { get; set; }        
        public double SalarioBruto { get; set; }
        public bool ControlaAlmoco { get; set; }
        public virtual IList<Ponto> Pontos { get; private set; } = new List<Ponto>();
        public virtual IList<ContraCheque> ContraCheques { get; private set; } = new List<ContraCheque>();
        public Guid IdPessoa { get; set; }
        public Pessoa Pessoa { get; set; }
        public bool Padrao { get; set; }

        public int DiaDeFechamento => 1;
        public double ValorHora
        {
            get
            {
                if (SalarioBruto.Equals(0))
                    return 0;
                else
                    return SalarioBruto / HoraMes;
            }
        }
        public TimeSpan SaldoBancoHoras
        {
            get
            {
                var total = TimeSpan.Zero;
                return !Pontos.Any(s => s.TipoDoEvento.Equals(TipoDoEvento.Entrada)) ? TimeSpan.Zero : Pontos.GroupBy(s => s.DataHora.Date).Aggregate(total, (current, item) => current + CalcularBancoHoras(item));
            }
        }

        public uint HoraMes => this.HoraMes.Equals(null) ? 220 : this.HoraMes;
        public double ValorBancoHoras => SaldoBancoHoras.TotalHours * ValorHora;
        public IEnumerable<Ponto> PontosDoDia => this.Pontos.Where(p => p.DataHora.CompareTo(DateTime.Today) >= 0);
        public TimeSpan CargaHorariaDiaria => this.HorarioDeEntrada - this.HorarioDeSaida;
        public TimeSpan TempoDeAlmoco => this.HorarioDeEntradaDoAlmoco.IsNotNull() || this.HorarioDeSaidaDoAlmoco.IsNotNull() ? 
            HorarioDeEntradaDoAlmoco.Value - HorarioDeSaidaDoAlmoco.Value : new TimeSpan(0, 0, 0, 0);

        #endregion

        #region [ Métodos Privados ]

        private Ponto GetPonto(IEnumerable<Ponto> pontos, TipoDoEvento tipoDoEvento)
        {
            return pontos.LastOrDefault(s => s.TipoDoEvento.Equals(tipoDoEvento));
        }
        private TimeSpan CalcularBancoHoras(IGrouping<DateTime, Ponto> pontos)
        {
            var entrada = GetPonto(pontos, TipoDoEvento.Entrada).DataHora.TimeOfDay;
            var pontoSaida = GetPonto(pontos, TipoDoEvento.Saida);
            var saida = pontoSaida?.DataHora.TimeOfDay ?? DateTime.Now.TimeOfDay;

            var cargaHoraria = saida.Subtract(entrada).Subtract(this.CargaHorariaDiaria);

            var pontoEntradaAlmoco = GetPonto(pontos, TipoDoEvento.EntradaDoAlmoco);

            if (!this.ControlaAlmoco) return cargaHoraria.Subtract(this.TempoDeAlmoco);
            if (pontoEntradaAlmoco == null && this.ControlaAlmoco) return cargaHoraria;
          
            var entradaAlmoco = pontoEntradaAlmoco?.DataHora.TimeOfDay ?? TimeSpan.Zero;
            var pontoSaidaAlmoco = GetPonto(pontos, TipoDoEvento.SaidaDoAlmoco);
            var saidaAlmoco = pontoSaidaAlmoco?.DataHora.TimeOfDay ?? TimeSpan.Zero;

            var tempoAlmoco = saidaAlmoco.Subtract(entradaAlmoco).Subtract(this.TempoDeAlmoco);

            return cargaHoraria.Subtract(this.TempoDeAlmoco).Subtract(tempoAlmoco);
        }

        #endregion

        #region [ Métodos Públicos ]

        public void AdicionarPonto(Ponto ponto)
        {
            this.Pontos.Add(ponto);
        }
        public void AdicionarPonto(TipoDoEvento tipoDoEvento, DateTime? dataHora = null)
        {
            var ponto = PontoFactory.Criar(tipoDoEvento, dataHora);        
            this.AdicionarPonto(ponto);
        }
        public ContraCheque GerarContraCheque(int mes)
        {
            var contraCheque = ContraChequeFactory.Criar(this,
                new DateTime(this.DiaDeFechamento, mes, DateTime.Today.Year));
            //this.ContraCheques.Add(contraCheque);

            return contraCheque;
        }

        #endregion
    }
}