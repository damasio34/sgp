using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class TrabalhoTypeConfiguration : EntidadeBaseTypeConfiguration<Trabalho>
    {
        public TrabalhoTypeConfiguration() : base()
        {
            Property(p => p.MesesDoCiclo).IsRequired();
            Property(p => p.HorarioDeEntrada).IsRequired();
            Property(p => p.HorarioDeSaida).IsRequired();
            Property(p => p.SalarioBruto).IsRequired();
            Property(p => p.ControlaAlmoco).IsRequired();

            HasRequired(p => p.Pessoa).WithMany();
        }
    }
}