using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
                
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
    }
}
