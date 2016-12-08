using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class LancamentoDoContrachequeTypeConfiguration : EntidadeBaseTypeConfiguration<LancamentoDoContracheque>
    {
        public LancamentoDoContrachequeTypeConfiguration() : base()
        {
            Property(p => p.Descricao).IsRequired();
            Property(p => p.Valor).IsRequired();
            Property(p => p.TipoDeLancamento).IsRequired();

            HasRequired(p => p.ContraCheque).WithMany(p => p.Lancamentos).HasForeignKey(p => p.IdContraCheque);
        }
    }
}
