using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsDbLibrary.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetByID(params object[] keyValues);
        void Edit(T entity);
        void Insert(T entity);
        void Delete(T entity);
    }
}
