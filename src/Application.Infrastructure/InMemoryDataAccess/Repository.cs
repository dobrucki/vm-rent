using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Ports;

namespace Application.Infrastructure.InMemoryDataAccess
{
    public class Repository<T> : IRepository<T> where T : ModelBase
    {
        private readonly ConcurrentDictionary<Guid, T> _context;

        public Repository()
        {
            _context = new ConcurrentDictionary<Guid, T>();
        }
        
        public async Task CreateAsync(T entity)
        {
            var id = Guid.NewGuid();
            entity.Id = id;
            _context.TryAdd(id, entity);
        }

        public async Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Values.Where(predicate.Compile()).AsEnumerable();
        }
    }
}