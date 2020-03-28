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
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task CreateAsync(T virtualMachine);
        Task UpdateAsync(T virtualMachine);
        Task DeleteAsync(T entity);

    }
}