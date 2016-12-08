using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class PontoTypeConfiguration : EntidadeBaseTypeConfiguration<Ponto>
    {
        public PontoTypeConfiguration() : base()
        {
            HasRequired(p => p.Ciclo).WithMany(p => p.Pontos).HasForeignKey(p => p.IdCiclo);
        }
    }
}
