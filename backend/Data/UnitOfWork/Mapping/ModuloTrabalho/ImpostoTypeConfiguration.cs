﻿using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho
{
    class ImpostoTypeConfiguration : EntidadeBaseTypeConfiguration<Imposto>
    {
        public ImpostoTypeConfiguration() : base()
        {
            Property(p => p.Valor).IsRequired();
            Property(p => p.TipoDoImposto).IsRequired();
        }
    }
}
