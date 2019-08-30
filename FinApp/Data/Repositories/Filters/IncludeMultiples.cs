using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories.Filters
{
    public static class IncludeMultiples
    {
        public static IQueryable<TEntity> IncludeMultiple<TEntity>
            (this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }

        public static IQueryable<TEntity> WhereIncludeMultiple<TEntity>
            (this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            query = query.Where(predicate);
            return IncludeMultiple(query, includes);
        }

        public static async Task<TEntity> IncludeMultiple<TEntity>
            (this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            return await WhereIncludeMultiple(query, predicate, includes).FirstOrDefaultAsync();
        }
    }
}
