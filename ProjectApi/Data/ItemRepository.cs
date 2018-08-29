using ProjectApi.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data.Models;

namespace ProjectApi.Data
{
    public class GroupRepository : Repository<Models.Item>
    {
        public GroupRepository(IDbContext dbContext) : base(dbContext)
        {
            
        }

        public override IQueryable<Item> GetAll()
        {
            return dbSet.
                Include(item => item.Tags);
        }
                
        public override Item Get<TKey>(TKey id)
        {
            return this.dbSet.
                Include(item => item.Tags).
                Where(item => item.Id == Convert.ToInt64(id)).
                FirstOrDefault();     
        }

        public override Item Add(Item entity)
        {
            base.Add(entity);
            Commit();

            return entity;
        }

        public override bool Delete<TKey>(TKey id)
        {
            var entity = this.dbSet.
                Include(item => item.Tags).
                Where(item => item.Id == Convert.ToInt64(id)).
                FirstOrDefault();

            this.dbSet.Remove(entity);
            Commit();

            return true;
            
        }

    }
}
