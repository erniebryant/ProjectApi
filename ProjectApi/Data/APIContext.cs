using ProjectApi.Data.Models;

namespace ProjectApi.Data
{
    using ProjectApi.Data.Core;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class APIContext : DbContext, IDbContext
    {
        private IDbContextTransaction _currentTransaction;


        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(ConfigurationManager.GetConnectionString("DefaultConnection"));
        }
               

        public DbSet<Item> Items { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Tag>().ToTable("Tag");
        }

        public void BeginTransaction()
        {
            try
            {
                if (_currentTransaction != null)
                {
                    return;
                }

                _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception)
            {
                // todo: log transaction exception
                throw;
            }
        }

        public void CloseTransaction()
        {
            CloseTransaction(exception: null);
        }

        public void CloseTransaction(Exception exception)
        {
            try
            {
                if (_currentTransaction != null && exception != null)
                {
                    // todo: log exception
                    _currentTransaction.Rollback();
                    return;
                }

                SaveChanges();

                if (_currentTransaction != null)
                {
                    _currentTransaction.Commit();
                }
            }
            catch (Exception)
            {
                // todo: log exception
                if (_currentTransaction != null && _currentTransaction.GetDbTransaction().Connection != null)
                {
                    _currentTransaction.Rollback();
                }

                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

    }

    

    

    

}