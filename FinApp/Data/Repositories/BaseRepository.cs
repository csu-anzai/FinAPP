using Data.Entities.Abstractions;
using Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    abstract public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _entity;
        protected DbContext context;

        public BaseRepository(DbContext context)
        {
            this.context = context;
            _entity = context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entities = await _entity.Where(expression).ToListAsync();
            return entities;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
             return await _entity.SingleOrDefaultAsync(i => i.Id == id);
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
                return;

             _entity.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> range)
        {
            _entity.RemoveRange(range);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entity = await _entity.SingleOrDefaultAsync(expression);
            return entity;
        }
    }
}
