using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class CicloTypeConfiguration : EntidadeBaseTypeConfiguration<Ciclo>
    {
        public CicloTypeConfiguration() : base()
        {
            Property(p => p.DataDeInicio).IsRequired();
            Property(p => p.DateDeTermino).IsRequired();
            Property(p => p.ControlaAlmoco).IsRequired();
            Property(p => p.CargaHorariaDiaria).IsRequired();
            Property(p => p.TempoDeAlmoco).IsRequired();

            HasRequired(p => p.Trabalho).WithMany(p => p.Ciclos).HasForeignKey(p => p.IdTrabalho);
        }
    }
}
