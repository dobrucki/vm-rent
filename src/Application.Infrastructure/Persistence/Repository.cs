using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Ports;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : ModelBase
    {
        protected readonly PostgresContext Context;

        public Repository(DbContext context)
        {
            Context = context as PostgresContext;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public Task RemoveAsync(TEntity entity)
        {
            var task = new Task(() => Context.Set<TEntity>().Remove(entity));
            return task;
        }

        public Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            var task = new Task(() => Context.Set<TEntity>().RemoveRange(entities));
            return task;
        }
    }
}