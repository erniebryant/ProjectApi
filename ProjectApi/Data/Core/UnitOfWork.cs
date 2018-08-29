using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjectApi.Data.Core
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IDbContext dbContext;

        private Dictionary<Type, object> repositories;

        public UnitOfWork(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!this.repositories.ContainsKey(type))
            {
                this.repositories[type] = GetRepositoryInstance<TEntity>();//  new Repository<TEntity>(this.dbContext);
            }

            return (IRepository<TEntity>)this.repositories[type];
        }

        public int Commit()
        {
            // Save changes with the default options
            return this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(obj: this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dbContext != null)
                {
                    this.dbContext.Dispose();
                    this.dbContext = null;
                }
            }
        }

        private Object GetRepositoryInstance<TEntity>() where TEntity : class
        {
            //Load all types in assembly
            Type repositoryType = typeof(Repository<TEntity>).Assembly.GetTypes().
                Where(t => t.IsSubclassOf(typeof(Repository<TEntity>))).
                First();

            if (repositoryType != null)
                return Activator.CreateInstance(repositoryType, this.dbContext);
            //Check for any derived repository

            return new Repository<TEntity>(this.dbContext);
            //if not use the generic type
            
        }
    }
}
