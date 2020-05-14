using System;
using System.Threading.Tasks;
using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Roles
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(Guid id);
        Task InsertOneAsync(Role role);
        Task UpdateOneAsync(Role role);
        Task DeleteOneAsync(Role role);
    }
}