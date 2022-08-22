using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Domain.Common;
using BlazorApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorApp.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }


        public async Task<IQueryable<T>> GetAllAsync()
        {
            var dbSet = _dbContext.Set<T>();
            return await Task.FromResult(dbSet.AsQueryable());
        }

        public async Task<IQueryable<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] propertySelectors)
        {
            IQueryable<T> items = await GetAllAsync();

            if (propertySelectors != null)
            {
                foreach (var includeProperty in propertySelectors)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public async Task<IQueryable<T>> FetchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] propertySelectors)
        {
            var dbSet = _dbContext.Set<T>().Where(expression);

            foreach (var includeProperty in propertySelectors)
            {
                dbSet = dbSet.Include(includeProperty);
            }

            return await Task.FromResult(dbSet);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IQueryable<T>> GetByIds<T>(params object[] ids)
        {
            return (IQueryable<T>)ids.Select( async id => await GetByIdAsync(id))
                      .Where(x => x != null)
                      .AsQueryable();
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> selector, params Expression<Func<T, object>>[] propertySelectors)
        {
            return await FirstAsync(selector, propertySelectors);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] propertySelectors)
        {
            var dbSet = _dbContext.Set<T>() as IQueryable<T>;

            if (propertySelectors != null)
            {
                foreach (var includeProperty in propertySelectors)
                {
                    dbSet = dbSet.Include(includeProperty);
                }
            }

            return predicate == null
                       ? await dbSet.FirstOrDefaultAsync()
                       : await dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().AnyAsync(expression);
        }

        public async Task<bool> AllAsync(Expression<Func<T, bool>> expression)
        {
            return  await _dbContext.Set<T>().AllAsync(expression);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression == null
                       ? await _dbContext.Set<T>().CountAsync()
                       : await _dbContext.Set<T>().CountAsync(expression);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

    }
}