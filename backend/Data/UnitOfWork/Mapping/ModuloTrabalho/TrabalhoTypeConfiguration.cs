using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class TrabalhoTypeConfiguration : EntidadeBaseTypeConfiguration<Trabalho>
    {
        public TrabalhoTypeConfiguration() : base()
        {
            HasRequired(p => p.Pessoa).WithMany(p => p.Trabalhos).HasForeignKey(p => p.IdPessoa);            
            HasMany(p => p.ContraCheques).WithMany();
        }
    }
}