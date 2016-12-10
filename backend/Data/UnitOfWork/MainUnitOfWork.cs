using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using Damasio34.GraphDiff;
using Damasio34.Seedwork.Aggregates;
using Damasio34.Seedwork.Repositories.Queryable;
using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Data.UnitOfWork.Initializers;
using Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloPessoa;
using Damasio34.SGP.Data.UnitOfWork.Mapping.ModuloTrabalho;

namespace Damasio34.SGP.Data.UnitOfWork
{
    public class MainUnitOfWork : DbContext, IUnitOfWork
    {
        protected ObjectContext ObjectContext => (this as IObjectContextAdapter).ObjectContext;

        #region [ Construtores ]

        public static MainUnitOfWork Create()
        {
            return new MainUnitOfWork();
        }

        public MainUnitOfWork() : base("MainUnitOfWork")
        {
            this.AggregateUpdateStrategy = new GraphdiffAggregateUpdateStrategy();
            Init();
        }

        protected void Init()
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;

            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MainUnitOfWork>());
            Database.SetInitializer(new DropCreateDatabaseAlwaysInitializer());
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            base.OnModelCreating(modelBuilder);

            //Database.SetInitializer(new CreateDatabaseIfNotExistsInitializer());
            //Database.SetInitializer(new DropCreateDatabaseAlwaysInitializer());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChangesInitializer());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Modulo Trabalho
            modelBuilder.Configurations.Add(new ContraChequeTypeConfiguration());
            modelBuilder.Configurations.Add(new TrabalhoTypeConfiguration());
            modelBuilder.Configurations.Add(new PontoTypeConfiguration());
            modelBuilder.Configurations.Add(new CicloTypeConfiguration());
            modelBuilder.Configurations.Add(new ImpostoTypeConfiguration());

            // Modulo Financeiro
            //modelBuilder.Configurations.Add(new BancoTypeConfiguration());
            //modelBuilder.Configurations.Add(new CategoriaTypeConfiguration());
            //modelBuilder.Configurations.Add(new ContaTypeConfiguration());
            modelBuilder.Configurations.Add(new LancamentoDoContraChequeTypeConfiguration());

            // Modulo Pessoa
            modelBuilder.Configurations.Add(new PessoaTypeConfiguration());
            modelBuilder.Configurations.Add(new UsuarioTypeConfiguration());
        }

        #region [ IUnitOfWork ]

        public virtual IQueryable<TEntidade> Set<TEntidade>() where TEntidade : class
        {
            return base.Set<TEntidade>();
        }

        public virtual void RegisterClean<TEntidade>(TEntidade obj) where TEntidade : class
        {
            this.Entry(obj).State = EntityState.Unchanged;
        }

        public virtual void RegisterNew<TEntidade>(TEntidade obj) where TEntidade : class
        {
            base.Set<TEntidade>().Add(obj);
        }

        public virtual void RegisterDirty<TEntidade>(TEntidade obj) where TEntidade : class
        {
            // Faz o select do item pela sua chave primária.
            var objNoContexto = ObjectContext.GetObjectByKey(ObjectContext.CreateEntityKey(ObjectContext.CreateObjectSet<TEntidade>().EntitySet.Name, obj));

            if (ReferenceEquals(obj, objNoContexto)) { }
            // ... Não faz nada, pois o objeto no contexto já está alterado.
            else
                // ...atualiza os valores escalares da instância no item informado.
                Entry((TEntidade)objNoContexto).CurrentValues.SetValues(obj);
        }

        public virtual void RegisterDeleted<TEntidade>(TEntidade obj) where TEntidade : class
        {
            base.Set<TEntidade>().Remove(obj);
        }

        public virtual void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var erros = ex.EntityValidationErrors.SelectMany(entity => entity.ValidationErrors).Select(erro => erro.ErrorMessage).ToArray();
                throw new DbEntityValidationException(String.Join("\n", erros), ex.EntityValidationErrors, ex.InnerException);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void Rollback()
        {
            // code from http://code.msdn.microsoft.com/How-to-undo-the-changes-in-00aed3c4

            // Undo the changes of the all entries. 
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;

                }
            }
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sql, parameters);
        }

        public List<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return base.Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public virtual IAggregateUpdateStrategy AggregateUpdateStrategy { get; private set; }

        #endregion

        #region [ IQueryableUnitOfWork ]

        public IQueryBuilder<TEntidade> CreateQueryBuilder<TEntidade>() where TEntidade : class
        {
            return new EntityQueryBuilder<TEntidade>();
        }

        #endregion
    }
}