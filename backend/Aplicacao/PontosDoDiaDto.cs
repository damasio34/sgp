using System;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao
{
    public class PontosDoDiaDto
    {
        public Guid IdTrabalho { get; set; }
        public DateTime? HorarioDeEntrada { get; set; }
        public DateTime? HorarioDeSaida { get; set; }
        public DateTime? HorarioDeEntradaDoAlmoco { get; set; }
        public DateTime? HorarioDeSaidaDoAlmoco { get; set; }
    }
}
