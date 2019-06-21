using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class StandardRepository<TContext> : RepositoryBase<TContext>, IStandardRepository where TContext : DbContext
    {
        protected StandardRepository(TContext context) : base(context)
        { }


        public virtual IEnumerable<TEntity> GetAll<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(null, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(null, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(null, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(null, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> GetPage<TEntity>(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            if (orderBy == null) orderBy = x => x.OrderBy(y => y.Id);

            var result = QueryDb(null, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetPageAsync<TEntity>(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            if (orderBy == null) orderBy = x => x.OrderBy(y => y.Id);

            var result = QueryDb(null, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        public virtual TEntity Get<TEntity>(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            return query.SingleOrDefault(x => x.Id == id);
        }

        public virtual async Task<TEntity> GetAsync<TEntity>(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual IEnumerable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(filter, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(filter, orderBy, includes);
            return await result.ToListAsync();
        }

        public virtual void Load<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(filter, orderBy, includes);
            result.Load();
        }

        public virtual async Task LoadAsync<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            var result = QueryDb(filter, orderBy, includes);
            await result.LoadAsync();
        }

        public virtual IEnumerable<TEntity> QueryPage<TEntity>(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            if (orderBy == null) orderBy = x => x.OrderBy(y => y.Id);

            var result = QueryDb(filter, orderBy, includes);
            return result.Skip(startRow).Take(pageLength).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> QueryPageAsync<TEntity>(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new()
        {
            if (orderBy == null) orderBy = x => x.OrderBy(y => y.Id);

            var result = QueryDb(filter, orderBy, includes);
            return await result.Skip(startRow).Take(pageLength).ToListAsync();
        }

        protected IQueryable<TEntity> QueryDb<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes) where TEntity : class, IEntityBase, new()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public void SetUnchanged<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new()
        {
            base.Context.Entry<TEntity>(entity).State = EntityState.Unchanged;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new()
        {
            Context.Set<TEntity>().Add(entity);
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new()
        {
            return Context.Set<TEntity>().Update(entity).Entity;
        }

        public virtual void Remove<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new()
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Deleted;
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Remove<TEntity>(Guid id) where TEntity : class, IEntityBase, new()
        {
            var entity = new TEntity() { Id = id };
            this.Remove(entity);
        }
    }
}
