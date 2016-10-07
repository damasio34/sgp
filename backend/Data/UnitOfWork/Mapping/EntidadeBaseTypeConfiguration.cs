using System.Data.Entity.ModelConfiguration;
using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping
{
    public abstract class EntidadeBaseTypeConfiguration<T> : EntityTypeConfiguration<T> where T : EntidadeBase
    {
        protected EntidadeBaseTypeConfiguration() : base()
        {
            HasKey(p => p.Id);

            Property(p => p.Ativo).IsRequired();
            Property(p => p.DataDeCadastro).IsRequired();            
        }
    }
}
