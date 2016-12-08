using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.Seedwork.Extensions;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class Ciclo : EntidadeBase
    {
        internal Ciclo() { }
        public Ciclo(DateTime dataDeInicio, DateTime dataDeTermino, bool controlaAlmoco, 
            TimeSpan cargaHorariaDiaria, TimeSpan tempoDoAlmoco)
        {
            this.DataDeInicio = dataDeInicio;
            this.DateDeTermino = dataDeTermino;
            this.ControlaAlmoco = controlaAlmoco;

            this.CargaHorariaDiaria = cargaHorariaDiaria;
            this.TempoDeAlmoco = tempoDoAlmoco;
        }

        public DateTime DataDeInicio { get; set; }
        public DateTime DateDeTermino { get; set; }
        public IList<Ponto> Pontos { get; set; } = new List<Ponto>();        
        public bool ControlaAlmoco { get; set; }
        public TimeSpan? TempoDeAlmoco { get; set; }
        public TimeSpan CargaHorariaDiaria { get; set; }
        
        public TimeSpan SaldoDeHoras => this.CalcularHorasTrabalhadasNoCliclo();
        public TimeSpan SaldoDeHorasExtras => this.SaldoDeHoras - this.CalcularHorasUteisDoCicloAteHoje();

        private TimeSpan CalcularHorasUteisDoCicloAteHoje()
        {
            var quantidadeDeDias = 0;
            var dataAtual = DataDeInicio.Date;
            var dataDeTermino = DateDeTermino.Date <= DateTime.Today.Date ? DateDeTermino.Date : DateTime.Today.Date;

            while (dataAtual <= dataDeTermino)
            {
                if (!dataAtual.DayOfWeek.Equals(DayOfWeek.Sunday) && !dataAtual.DayOfWeek.Equals(DayOfWeek.Saturday))
                {
                    quantidadeDeDias++;
                }
                dataAtual = dataAtual.AddDays(1);
            }

            var total = new TimeSpan((int) (quantidadeDeDias * CargaHorariaDiaria.TotalHours), 0, 0);
            return total;
        }
        private TimeSpan CalcularHorasTrabalhadasNoCliclo()
        {
            var banco = new TimeSpan(0, 0, 0);     
            foreach (var pontosDoDia in this.Pontos.GroupBy(p => p.DataHora.Date))
            {
                if (this.ControlaAlmoco)
                {
                    var entrada = pontosDoDia.Single(p => p.TipoDoEvento == TipoDoEvento.Entrada);
                    var entradaDoAlmoco = pontosDoDia.Single(p => p.TipoDoEvento == TipoDoEvento.EntradaDoAlmoco);
                    var saidaDoAlmoco = pontosDoDia.Single(p => p.TipoDoEvento == TipoDoEvento.SaidaDoAlmoco);
                    var saida = pontosDoDia.Single(p => p.TipoDoEvento == TipoDoEvento.Saida);

                    banco += entradaDoAlmoco.DataHora.TimeOfDay - entrada.DataHora.TimeOfDay;
                    banco += saida.DataHora.TimeOfDay - saidaDoAlmoco.DataHora.TimeOfDay;
                }
                else
                {
                    var entrada = pontosDoDia.Single(p => p.TipoDoEvento == TipoDoEvento.Entrada);
                    var saida = pontosDoDia.Single(p => p.TipoDoEvento == TipoDoEvento.Saida);
                    if (this.TempoDeAlmoco.IsNull()) banco += saida.DataHora.TimeOfDay - entrada.DataHora.TimeOfDay;
                    else banco += saida.DataHora.TimeOfDay - entrada.DataHora.TimeOfDay - TempoDeAlmoco.Value;
                }
            }

            return banco;
        }        
    }
}
