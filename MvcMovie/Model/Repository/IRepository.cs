using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Model.Models
{
    public interface IRepository<TEntity, TResultMdoel> where TEntity : class
    {
        public IEnumerable<TResultMdoel> GetAll();

        public TResultMdoel Find(int id);

        public TResultMdoel FirstOrDefault(Expression<Func<TEntity, bool>> func);

        public TResultMdoel Add(TEntity movie);

        public MovieViewModel Update(TEntity movie);

        public void Remove(int id);
    }
}
