﻿using System.Data.Entity;
using Damasio34.SGP.Data.UnitOfWork.SeedDatabase;

namespace Damasio34.SGP.Data.UnitOfWork.Initializers
{
    public class CreateDatabaseIfNotExistsInitializer : CreateDatabaseIfNotExists<MainUnitOfWork>
    {
        protected override void Seed(MainUnitOfWork unitOfWork)
        {
            new DadosIniciais().Seed(unitOfWork);
        }
    }
}
