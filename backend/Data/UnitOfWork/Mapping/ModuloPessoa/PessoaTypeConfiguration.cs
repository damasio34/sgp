using Damasio34.SGP.Dominio.ModuloPessoa;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloPessoa
{
    class PessoaTypeConfiguration : EntidadeBaseTypeConfiguration<Pessoa>
    {
        public PessoaTypeConfiguration() : base()
        {
            Property(p => p.Nome).IsRequired();
            Property(p => p.Cpf).IsRequired();
            Property(p => p.Email).IsRequired();
        }
    }
}
