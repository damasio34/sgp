using System;
using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class Ponto : EntidadeBase
    {
        protected internal Ponto() { }

        public Guid IdTrabalho { get; set; }
        public Trabalho Trabalho { get; set; }
        public TipoDoEvento TipoDoEvento { get; set; }
        public DateTime DataHora { get; set; }
        public string Justificativa { get; set; }
    }
}