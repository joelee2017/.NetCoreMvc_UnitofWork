using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Model.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(object id);
        TEntity Insert(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(object id);
        void Save();
    }
}
