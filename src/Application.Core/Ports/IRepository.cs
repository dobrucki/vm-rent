using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace Application.Core.Ports
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    }
}