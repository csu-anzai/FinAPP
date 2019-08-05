﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Helpers
{
    public static class IncludesMultiples
    {
        public static IQueryable<TEntity> IncludeMultiple<TEntity>
            (this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,(current, include) => current.Include(include));
            }
            return query;
        }

        public static IQueryable<TEntity> WhereIncludeMultiple<TEntity>
            (this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            query = query.Where(predicate);
            return IncludeMultiple(query, includes);
        }

        public static TEntity IncludeMultiple<TEntity>
            (this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            return WhereIncludeMultiple(query, predicate, includes).FirstOrDefault();
        }
    }
}
