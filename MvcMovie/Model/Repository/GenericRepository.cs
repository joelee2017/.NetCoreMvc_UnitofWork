﻿using Microsoft.EntityFrameworkCore;
using Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Model.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private MvcMovieContext _context = null;
        private DbSet<TEntity> table = null;

        public GenericRepository(MvcMovieContext _context)
        {
            this._context = _context;
            table = _context.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return table.AsEnumerable();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate)
        {
            return table.Where(predicate);
        }

        public TEntity GetById(object id)
        {
            return table.Find(id);
        }
        public TEntity Insert(TEntity obj)
        {
            var result = table.Add(obj);
            return result.Entity;
        }
        public TEntity Update(TEntity obj)
        {
           var resut = table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;

            return resut.Entity;
        }
        public void Delete(object id)
        {
            TEntity existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
