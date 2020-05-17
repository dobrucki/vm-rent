using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetUsersWhereAsync(Expression<Func<User, bool>> predicate);
    }
}            