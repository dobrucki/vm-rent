using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Domain.Models;
using Application.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EfCore
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : ModelBase
    {
        private readonly PostgresContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(PostgresContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.SingleOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Attach(entity));
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                await Task.Run(() => _dbSet.Attach(entity));
            }

            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            await DeleteAsync(entity);
        }
    }
}