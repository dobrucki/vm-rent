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
            _context.TryAdd(entity.Id, entity);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Values.Where(predicate.Compile()).AsEnumerable();
        }

        public void Dispose()
        {
        }
    }
}