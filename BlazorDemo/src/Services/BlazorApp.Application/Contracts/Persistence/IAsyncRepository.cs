using BlazorApp.Domain.Common;
using System.Linq.Expressions;

namespace BlazorApp.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : EntityBase
    {
        Task<IQueryable<T>> GetAllAsync();

        Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] propertySelectors);

        Task<IQueryable<T>> FetchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] propertySelectors);

        Task<T> GetByIdAsync(object id);

        Task<IQueryable<T>> GetByIds<T>(params object[] ids);

        Task<T> GetAsync(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] propertySelectors);

        Task<T> FirstAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] propertySelectors);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<bool> AllAsync(Expression<Func<T, bool>> expression);

        Task<int> CountAsync(Expression<Func<T, bool>> expression = null);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}