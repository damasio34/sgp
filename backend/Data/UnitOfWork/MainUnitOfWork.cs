using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Damasio34.SGP.Data.UnitOfWork.Initializers;
using Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloFinanceiro;
using Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloPessoa;
using Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork
{
    public class MainUnitOfWork : UnitOfWork
    {
        #region [ Construtores ]

        public MainUnitOfWork()
            : base()
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        public MainUnitOfWork(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        public MainUnitOfWork(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        public MainUnitOfWork(DbConnection existingConnection)
            : base(existingConnection, true)
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        #endregion

        public override int SaveChanges()
        {
            return base.SaveChanges();

            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException dex)
            {
                throw dex;
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Database.SetInitializer(new CreateDatabaseIfNotExistsInitializer());
            //Database.SetInitializer(new DropCreateDatabaseAlwaysInitializer());
            Database.SetInitializer(new DropCreateDatabaseIfModelChangesInitializer());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Modulo Emprego
            modelBuilder.Configurations.Add(new ContraChequeTypeConfiguration());
            //modelBuilder.Configurations.Add(new EmpregoTypeConfiguration());
            modelBuilder.Configurations.Add(new PontoTypeConfiguration());

            // Modulo Financeiro
            modelBuilder.Configurations.Add(new BancoTypeConfiguration());
            modelBuilder.Configurations.Add(new CategoriaTypeConfiguration());
            modelBuilder.Configurations.Add(new ContaTypeConfiguration());
            modelBuilder.Configurations.Add(new LancamentoTypeConfiguration());
            // Modulo Pessoa
            modelBuilder.Configurations.Add(new PessoaTypeConfiguration());
            modelBuilder.Configurations.Add(new UsuarioTypeConfiguration());            
        }
    }
}