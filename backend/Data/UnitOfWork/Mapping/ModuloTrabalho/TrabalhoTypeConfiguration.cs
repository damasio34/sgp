using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class TrabalhoTypeConfiguration : EntidadeBaseTypeConfiguration<Trabalho>
    {
        public TrabalhoTypeConfiguration() : base()
        {
            HasRequired(p => p.Usuario).WithMany(p => p.Trabalhos).HasForeignKey(p => p.IdUsuario);

            HasMany(p => p.Pontos).WithMany();
            HasMany(p => p.ContraCheques).WithMany();
        }
    }
}