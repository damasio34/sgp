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

            HasMany(p => p.Pontos).WithRequired(p => p.Ciclo).HasForeignKey(p => p.IdCiclo);
            HasRequired(p => p.Trabalho).WithMany(p => p.Ciclos).HasForeignKey(p => p.IdTrabalho);
            //HasOptional(p => p.ContraCheque).WithOptionalDependent().Map(p => p.MapKey("IdContraCheque"));
        }
    }
}
