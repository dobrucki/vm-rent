using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserServiceContext context, ILogger<UserRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public IUnitOfWork UnitOfWork => _context;
        public async Task InsertAsync(User user)
        {
            _logger.LogCritical("Insert User invoked");
            await _context.Users.AddAsync(user);
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersWhereAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}