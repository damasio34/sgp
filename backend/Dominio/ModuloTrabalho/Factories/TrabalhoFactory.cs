using System;
using Damasio34.SGP.Dominio.ModuloPessoa;

namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class TrabalhoFactory
    {
        public static Trabalho Criar(Pessoa pessoa, double salario = 0, TimeSpan? horarioDeEntrada = null, 
            TimeSpan? horarioDeSaida = null, int mesesDoCliclo = 1, TimeSpan? horarioDeEntradaDoAlmoco = null,
            TimeSpan? horarioDeSaidaDoAlmoco = null)
        {
            horarioDeEntrada = horarioDeEntrada ?? new TimeSpan(9, 0, 0);
            horarioDeSaida = horarioDeSaida ?? new TimeSpan(18, 0, 0);
            horarioDeEntradaDoAlmoco = horarioDeEntradaDoAlmoco ?? new TimeSpan(12, 0, 0);
            horarioDeSaidaDoAlmoco = horarioDeSaidaDoAlmoco ?? new TimeSpan(13, 0, 0);

            var trabalho = new Trabalho(pessoa, salario, horarioDeEntrada.Value, horarioDeSaida.Value, mesesDoCliclo,
                horarioDeEntradaDoAlmoco.Value, horarioDeSaidaDoAlmoco.Value);
            trabalho.GerarId();
            return trabalho;
        }
    }
}
