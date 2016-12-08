using Damasio34.SGP.Dominio.ModuloPessoa;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloPessoa
{
    class UsuarioTypeConfiguration : EntidadeBaseTypeConfiguration<Usuario>
    {
        public UsuarioTypeConfiguration() : base()
        {
            Property(p => p.Login).IsRequired();
            Property(p => p.Senha).IsRequired();

            HasRequired(p => p.Pessoa).WithMany().HasForeignKey(p => p.IdPessoa);
        }
    }
}
