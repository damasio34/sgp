using System;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Dtos
{
    public class ConfiguracaoDto
    {
        public Guid IdTrabalho { get; set; }        
        public TimeSpan HorarioDeEntrada { get; set; }
        public TimeSpan HorarioDeSaida { get; set; }
        public TimeSpan? HorarioDeEntradaDoAlmoco { get; set; }
        public TimeSpan? HorarioDeSaidaDoAlmoco { get; set; }
        public int MesesDoCiclo { get; set; }
        public double SalarioBruto { get; set; }
        public bool ControlaAlmoco { get; set; }
    }
}
