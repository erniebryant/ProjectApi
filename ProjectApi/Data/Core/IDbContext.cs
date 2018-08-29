using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectApi.Data.Core
{
    public interface IDbContext
    {
        int SaveChanges();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        void Dispose();
    }
}
