using System;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Dtos
{
    public class PontoDto
    {
        public Guid Id { get; set; }
        public TipoDoEvento TipoDoEvento { get; set; }
        public DateTime DataHora { get; set; }
        public string Dia => DataHora.ToShortDateString();
        public string Justificativa { get; set; }
    }
}
