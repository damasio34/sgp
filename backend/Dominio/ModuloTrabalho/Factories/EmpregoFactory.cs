using System;

namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class EmpregoFactory
    {
        public static Trabalho Criar()
        {
            var emprego = new Trabalho();
            emprego.HoraMes = emprego.HoraMes.Equals(null) ? 220 : emprego.HoraMes;
            emprego.SalarioBruto = emprego.SalarioBruto.Equals(null) ? 0 : emprego.SalarioBruto;
            return emprego;
        }
        public static Trabalho Criar(TimeSpan cargaHorariaDiaria, TimeSpan tempoAlmoco)
        {
            var emprego = Criar();
            emprego.CargaHorariaDiaria = cargaHorariaDiaria;
            emprego.TempoAlmoco = tempoAlmoco;
            return emprego;
        }
        public static Trabalho Criar(TimeSpan cargaHorariaTrabalho, TimeSpan tempoAlmoco, TimeSpan entrada, TimeSpan saida)
        {
            var emprego = Criar(cargaHorariaTrabalho, tempoAlmoco);
            emprego.HorarioEntrada = entrada;
            emprego.HorarioSaida = saida;

            return emprego;
        }
        public static Trabalho Criar(TimeSpan cargaHorariaTrabalho, TimeSpan tempoAlmoco, TimeSpan entrada, TimeSpan saida, Decimal salarioBruto)
        {
            var emprego = Criar(cargaHorariaTrabalho, tempoAlmoco, entrada, saida);
            emprego.SalarioBruto = salarioBruto;

            return emprego;
        }
        public static Trabalho Criar(TimeSpan cargaHorariaTrabalho, TimeSpan tempoAlmoco, TimeSpan entrada, TimeSpan saida, Decimal valorHora, uint horaMes)
        {
            var emprego = Criar(cargaHorariaTrabalho, tempoAlmoco, entrada, saida, valorHora);
            emprego.HoraMes = horaMes;

            return emprego;
        }
    }
}
