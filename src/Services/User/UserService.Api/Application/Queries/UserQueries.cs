using System;
using System.Threading.Tasks;
using UserService.Api.Application.Helpers;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Api.Application.Queries
{
    public class UserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<User> AuthenticationQuery(string emailAddress, string password)
        {
            var passwordHash = Hashing.HashPassword(password);
            return await _userRepository
                .GetUserForCredentialsAsync(emailAddress, passwordHash) ?? throw new Exception();
        }
    }
}