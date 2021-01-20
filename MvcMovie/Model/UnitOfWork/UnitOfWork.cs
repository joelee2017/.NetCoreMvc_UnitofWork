using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Repository;
using System;
using System.Collections;

namespace Model.UnitOfWork
{
    public class UnitOfWork<C> : IDisposable, IUnitOfWork where C : MvcMovieContext, new()
    {
        private C context = new C();
        private Hashtable repositories = new Hashtable();
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.Contains(typeof(T)))
            {
                repositories.Add(typeof(T), new GenericRepository<T>(context));
            }
            return (IGenericRepository<T>)repositories[typeof(T)];
        }
        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
