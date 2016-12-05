using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.ModuloPessoa;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class Trabalho : EntidadeBase
    {
        #region [ Contrutores ]

        public Trabalho() {}
        internal Trabalho(double salario, TimeSpan cargaHorariaTrabalho, TimeSpan tempoAlmoco, 
            TimeSpan entrada, TimeSpan saida, uint mesesDoCliclo)
        {
            this.SalarioBruto = salario;
            this.CargaHorariaDiaria = cargaHorariaTrabalho;
            this.TempoAlmoco = tempoAlmoco;
            this.HorarioDeEntrada = entrada;
            this.HorarioDeSaida = saida;
            this.MesesCiclo = mesesDoCliclo;

            this.ControlaAlmoco = true;
            this.Padrao = true;
        }

        #endregion

        #region [ Propriedades ]

        public uint MesesCiclo { get; set; }
        public TimeSpan HorarioDeEntrada { get; set; }
        public TimeSpan HorarioDeSaida { get; set; }
        public TimeSpan CargaHorariaDiaria { get; set; }        
        public TimeSpan TempoAlmoco { get; set; }        
        public double SalarioBruto { get; set; }
        public bool ControlaAlmoco { get; set; }
        public double ValorHora { 
            get {
                if (SalarioBruto.Equals(0))
                    return 0;
                else
                    return SalarioBruto / HoraMes;
            }
        }
        public virtual IList<Ponto> Pontos { get; private set; } = new List<Ponto>();
        public virtual IList<ContraCheque> ContraCheques { get; private set; } = new List<ContraCheque>();
        public TimeSpan SaldoBancoHoras
        {
            get
            {
                var total = TimeSpan.Zero;
                return !Pontos.Any(s => s.TipoDoEvento.Equals(TipoDoEvento.Entrada)) ? TimeSpan.Zero : Pontos.GroupBy(s => s.DataHora.Date).Aggregate(total, (current, item) => current + CalcularBancoHoras(item));
            }
        }
        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public bool Padrao { get; set; }

        public uint HoraMes => this.HoraMes.Equals(null) ? 220 : this.HoraMes;
        public double ValorBancoHoras => SaldoBancoHoras.TotalHours * ValorHora;
        public IEnumerable<Ponto> PontosDoDia => this.Pontos.Where(p => p.DataHora.CompareTo(DateTime.Today) >= 0);

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

            if (!this.ControlaAlmoco) return cargaHoraria.Subtract(this.TempoAlmoco);
            if (pontoEntradaAlmoco == null && this.ControlaAlmoco) return cargaHoraria;
          
            var entradaAlmoco = pontoEntradaAlmoco?.DataHora.TimeOfDay ?? TimeSpan.Zero;
            var pontoSaidaAlmoco = GetPonto(pontos, TipoDoEvento.SaidaDoAlmoco);
            var saidaAlmoco = pontoSaidaAlmoco?.DataHora.TimeOfDay ?? TimeSpan.Zero;

            var tempoAlmoco = saidaAlmoco.Subtract(entradaAlmoco).Subtract(this.TempoAlmoco);

            return cargaHoraria.Subtract(this.TempoAlmoco).Subtract(tempoAlmoco);
        }

        #endregion

        #region [ Métodos Públicos ]

        public void AdicionarPonto(TipoDoEvento tipoDoEvento)
        {
            var ponto = new Ponto
            {
                DataHora = DateTime.Now,
                TipoDoEvento = tipoDoEvento
            };
            ponto.GerarId();
            this.Pontos.Add(ponto);
        }

        #endregion
    }
}