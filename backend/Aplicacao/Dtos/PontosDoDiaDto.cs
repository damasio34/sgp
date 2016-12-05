using System;

namespace Damasio34.SGP.Aplicacao.Dtos
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
