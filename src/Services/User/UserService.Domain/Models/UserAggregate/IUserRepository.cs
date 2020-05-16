using System.Threading.Tasks;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task InsertAsync(User user);
        Task<User> GetUserForCredentialsAsync(string emailAddress, string passwordHash);
    }
}            