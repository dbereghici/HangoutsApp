using HangoutsDbLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Repository
{
    public class UnitOfWork : IDisposable
    {
        private HangoutsContext context;
        public UnitOfWork()
        {
            context = new HangoutsContext();
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(context);
        }

        public void Dispose()
        {
            context.Dispose();
            //GC.SuppressFinalize(this);
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
