using System.Data.Entity.Migrations;
using Damasio34.SGP.Data.UnitOfWork.SeedDatabase;

namespace Damasio34.SGP.Data.Migrations
{    
    internal sealed class Configuration : DbMigrationsConfiguration<UnitOfWork.MainUnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UnitOfWork.MainUnitOfWork context)
        {
            new DadosIniciais().Seed(context);
        }
    }
}
