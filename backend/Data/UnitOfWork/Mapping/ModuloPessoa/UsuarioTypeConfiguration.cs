using Damasio34.SGP.Dominio.ModuloPessoa;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloPessoa
{
    class UsuarioTypeConfiguration : EntidadeBaseTypeConfiguration<Usuario>
    {
        public UsuarioTypeConfiguration() : base()
        {
            HasRequired(p => p.Pessoa).WithMany().HasForeignKey(p => p.IdPessoa);
        }
    }
}
