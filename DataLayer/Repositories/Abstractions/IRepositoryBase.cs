using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataLayer.Entities.Base;

namespace DataLayer.Repositories.Abstractions
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IBaseEntity
    {
        TEntity GetSingleByPredicate(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);

        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
