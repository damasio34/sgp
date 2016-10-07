using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class Trabalho : EntidadeBase
    {
        #region [ Contrutores ]

        protected internal Trabalho() { 
            Pontos = new List<Ponto>();
            ControlaAlmoco = true;
        }

        #endregion

        #region [ Propriedades ]

        public int MesesCiclo { get; set; }
        public TimeSpan HorarioEntrada { get; set; }
        public TimeSpan HorarioSaida { get; set; }
        public TimeSpan CargaHorariaDiaria { get; set; }        
        public TimeSpan TempoAlmoco { get; set; }
        public uint HoraMes { get; set; }
        public decimal SalarioBruto { get; set; }
        public bool ControlaAlmoco { get; set; }
        public decimal ValorHora { 
            get {
                if (SalarioBruto.Equals(0))
                    return 0;
                else
                    return SalarioBruto / HoraMes;
            }
        }
        public virtual ICollection<Ponto> Pontos { get; private set; }
        public virtual ICollection<ContraCheque> ContraCheques { get; set; }
        public TimeSpan SaldoBancoHoras
        {
            get
            {
                var total = TimeSpan.Zero;

                if (!Pontos.Any(s => s.TipoDoEvento.Equals(TipoDoEvento.Entrada)))
                    return TimeSpan.Zero;

                return Pontos.GroupBy(s => s.DataHora.Date).Aggregate(total, (current, item) => current + CalcularBancoHoras(item));
            }
        }
        public decimal ValorBancoHoras => (decimal) SaldoBancoHoras.TotalHours * ValorHora;

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
            var saida = pontoSaida != null ? pontoSaida.DataHora.TimeOfDay : DateTime.Now.TimeOfDay;

            var cargaHoraria = saida.Subtract(entrada).Subtract(this.CargaHorariaDiaria);

            var pontoEntradaAlmoco = GetPonto(pontos, TipoDoEvento.EntradaAlmoco);

            if (!this.ControlaAlmoco) return cargaHoraria.Subtract(this.TempoAlmoco);
            if (pontoEntradaAlmoco == null && this.ControlaAlmoco) return cargaHoraria;
          
            var entradaAlmoco = pontoEntradaAlmoco != null ? pontoEntradaAlmoco.DataHora.TimeOfDay : TimeSpan.Zero;
            var pontoSaidaAlmoco = GetPonto(pontos, TipoDoEvento.SaidaAlmoco);
            var saidaAlmoco = pontoSaidaAlmoco != null ? pontoSaidaAlmoco.DataHora.TimeOfDay : TimeSpan.Zero;

            var tempoAlmoco = saidaAlmoco.Subtract(entradaAlmoco).Subtract(this.TempoAlmoco);

            return cargaHoraria.Subtract(this.TempoAlmoco).Subtract(tempoAlmoco);
        }

        #endregion

        #region [ Métodos Públicos ]

        public void AdicionarPonto(Ponto ponto)
        {
            if (this.Pontos == null)
                this.Pontos = new List<Ponto>();

            this.Pontos.Add(ponto);
        }

        #endregion
    }
}