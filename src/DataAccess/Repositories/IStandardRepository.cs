using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IStandardRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        void Load<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        Task LoadAsync<TEntity>(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        IEnumerable<TEntity> GetPage<TEntity>(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        Task<IEnumerable<TEntity>> GetPageAsync<TEntity>(int startRow, int pageLength, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        TEntity Get<TEntity>(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        Task<TEntity> GetAsync<TEntity>(Guid id, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        IEnumerable<TEntity> Query<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        void Load<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        Task LoadAsync<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        IEnumerable<TEntity> QueryPage<TEntity>(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        Task<IEnumerable<TEntity>> QueryPageAsync<TEntity>(int startRow, int pageLength, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null) where TEntity : class, IEntityBase, new();
        void SetUnchanged<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new();
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new();
        TEntity Update<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new();
        void Remove<TEntity>(TEntity entity) where TEntity : class, IEntityBase, new();
        void Remove<TEntity>(Guid id) where TEntity : class, IEntityBase, new();
    }
}
