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
        protected PostgresContext Context;
        protected DbSet<TEntity> DbSet;

        public Repository(PostgresContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.SingleOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => DbSet.Attach(entity));
            Context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                await Task.Run(() => DbSet.Attach(entity));
            }

            DbSet.Remove(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            await DeleteAsync(entity);
        }
    }
}