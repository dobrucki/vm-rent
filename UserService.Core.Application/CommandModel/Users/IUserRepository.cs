using System;
using System.Threading.Tasks;
using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Users
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(Guid id);
        public Task InsertOneAsync(User user);
        public Task UpdateOneAsync(User user);
    }
}