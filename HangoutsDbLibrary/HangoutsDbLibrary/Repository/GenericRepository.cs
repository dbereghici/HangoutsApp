using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsDbLibrary.Repository
{
    class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public DbContext context;
        public DbSet<T> dbset;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbset = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return dbset;
        }

        public T GetByID(int id)
        {
            return dbset.Find(id);
        }

        public void Edit(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(T entity)
        {
            dbset.Add(entity);
            //this.context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }
    }
}
