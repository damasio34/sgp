using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class ContraChequeTypeConfiguration : EntidadeBaseTypeConfiguration<ContraCheque>
    {
        public ContraChequeTypeConfiguration() : base()
        {
            Property(p => p.ValorBruto).IsRequired();

            HasRequired(p => p.Ciclo).WithMany();
        }
    }
}
