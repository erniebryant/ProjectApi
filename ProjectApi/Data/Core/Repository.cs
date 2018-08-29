using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ProjectApi.Data.Core
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T>
       where T : class
    {
        //EF context
        private readonly IDbContext context;
        
        //Used to query/save
        protected readonly DbSet<T> dbSet;
        
        public Repository(IDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        /// <summary>
        /// Add new element to Repository
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Add(T entity)
        {
            this.dbSet.Add(entity);
            return entity;
        }

        /// <summary>
        /// Get Element by Id
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get<TKey>(TKey id)
        {
            return this.dbSet.Find(id);
        }

        /// <summary>
        /// Get All elements
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return this.dbSet;
        }

        /// <summary>
        /// Get element async
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<TKey>(TKey id)
        {
            return await this.dbSet.FindAsync(id);
        }

        /// <summary>
        /// Delete an element
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EntityState Delete(T entity)
        {
            return this.dbSet.Remove(entity).State;
        }

        /// <summary>
        /// Delete element by Id
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public EntityState Delete<TKey>(TKey id)
        {
            var elem = this.dbSet.Find(id);
            return this.dbSet.Remove(elem).State;
        }

        /// <summary>
        /// Update an element
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityState Update(T entity)
        {
            return this.dbSet.Update(entity).State;
        }

        protected virtual int Commit()
        {
            return this.context.SaveChanges();
        }
    }
}
