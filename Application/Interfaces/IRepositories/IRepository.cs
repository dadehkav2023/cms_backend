using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> DeferdSelectAll();
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity, bool autoSave);
        Task<TEntity> FirstOrDefaultItemAsync(Expression<Func<TEntity, bool>> condition);

        IQueryable<TEntity> DeferredWhere(Expression<Func<TEntity, bool>> condition);

        IQueryable<TEntity> DeferredWhere(Expression<Func<TEntity, bool>> condition, string orderByProperties);

        IQueryable<TEntity> DeferredWhere(Expression<Func<TEntity, bool>> condition, int page, int pageSize);

        IQueryable<TEntity> DeferredWhere(Expression<Func<TEntity, bool>> condition, string orderByProperties, int page, int pageSize);

        TEntity Find(params object[] keyValue);

        //Task<TEntity> FindAsync(params object[] keyValue);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave);
        Task<TEntity> RemoveAsync(TEntity entity, bool autoSave);
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> condition = null);

        void Dispose();
    }
}
