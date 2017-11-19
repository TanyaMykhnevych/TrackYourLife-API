using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataLayer.DbContext;
using DataLayer.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Common.Utils;
using DataLayer.Repositories.Abstractions;

namespace DataLayer.Repositories.Implementations
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected readonly AppDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(
            AppDbContext dbContext, DbSet<TEntity> dbSet)
        {
            DbContext = dbContext;
            DbSet = dbSet;
        }

        protected IQueryable<TEntity> Queryable => DbSet.AsQueryable();

        public virtual TEntity GetSingleByPredicate(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = Queryable;

            if (include != null)
            {
                query = include(query);
            }

            return query.SingleOrDefault(predicate);
        }

        public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = Queryable;

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? Queryable.Any(predicate) : Queryable.Any();
        }

        public virtual TEntity Add(TEntity entity)
        {
            string currentUser = CurrentUserHolder.GetCurrentUserName();

            entity.CreatedBy = currentUser;
            entity.Created = DateTime.UtcNow;

            var entityEntry = DbSet.Add(entity);
            DbContext.SaveChanges();

            return entityEntry.Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            string currentUser = CurrentUserHolder.GetCurrentUserName();

            entity.UpdatedBy = currentUser;
            entity.Updated = DateTime.UtcNow;

            var entityEntry = DbSet.Update(entity);
            DbContext.SaveChanges();

            return entityEntry.Entity;
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }
    }
}
