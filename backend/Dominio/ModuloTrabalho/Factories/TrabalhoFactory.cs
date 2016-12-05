using System;

namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class TrabalhoFactory
    {
        public static Trabalho Criar(double salario = 0, TimeSpan? cargaHorariaTrabalho = null, 
            TimeSpan? tempoAlmoco = null, TimeSpan? horarioDeEntrada = null, TimeSpan? horarioDeSaida = null, uint mesesDoCliclo = 1)
        {
            cargaHorariaTrabalho = cargaHorariaTrabalho ?? new TimeSpan(8, 0, 0);
            tempoAlmoco = tempoAlmoco ?? new TimeSpan(1, 0, 0);
            horarioDeEntrada = horarioDeEntrada ?? new TimeSpan(9, 0, 0);
            horarioDeSaida = horarioDeSaida ?? new TimeSpan(18, 0, 0);
            
            var trabalho = new Trabalho(salario, cargaHorariaTrabalho.Value, tempoAlmoco.Value, 
                horarioDeEntrada.Value, horarioDeSaida.Value, mesesDoCliclo);
            trabalho.GerarId();
            return trabalho;
        }
    }
}
