using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Application.Core.Models;


namespace Application.Core.Ports
{
    public interface IRepository<T> where T : ModelBase
    { 
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        Task AddAsync(T virtualMachine);
        Task AddRangeAsync(IEnumerable<T> entities);

        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);


    }
}