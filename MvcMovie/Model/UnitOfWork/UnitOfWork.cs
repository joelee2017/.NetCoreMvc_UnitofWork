using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.Repository;
using System;
using System.Collections;

namespace Model.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private MvcMovieContext _context;
        private Hashtable repositories = new Hashtable();

        public UnitOfWork(MvcMovieContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.Contains(typeof(T)))
            {
                repositories.Add(typeof(T), new GenericRepository<T>(_context));
            }
            return (IGenericRepository<T>)repositories[typeof(T)];
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
