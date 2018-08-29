using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectApi.Data.Core
{
    public interface IRepository<T>
    {

        T Get<TKey>(TKey id);

        IQueryable<T> GetAll();

        Task<T> GetAsync<TKey>(TKey id);

        //GetAsyncAll?
        
        T Add(T entity);
                
        EntityState Delete(T entity);

        EntityState Delete<TKey>(TKey id);

        EntityState Update(T entity);
    }
}
