using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Application.Helpers;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.Queries
{
    public class UserQueries
    {
        private readonly IUserRepository _userRepository;

        public UserQueries(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<User> AuthenticateUserQuery(string emailAddress, string password)
        {
            var passwordHash = Hashing.HashPassword(password);
            var user = (await _userRepository
                .GetUsersWhereAsync(x => emailAddress == x.EmailAddress && passwordHash == x.Password))
                .FirstOrDefault() ?? throw new Exception();
            return user;
        }

        public async Task<User> GetUserByIdQuery(Guid id)
        {
            var user = await _userRepository
                .GetUserByIdAsync(id) ?? throw new Exception();
            return user;
        }

        public async Task<User> GetUserByEmailAddressQuery(string emailAddress)
        {
            var user = (await _userRepository
                .GetUsersWhereAsync(x => emailAddress == x.EmailAddress))
                .FirstOrDefault() ?? throw new Exception();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersQuery(OffsetPagination pagination)
        {
            var users = await _userRepository.GetUsersWhereAsync(x => true);
            return users.Paginate(pagination);
        }

        public async Task<IEnumerable<User>> GetUsersWhereNameContainsPhraseQuery(string phrase, OffsetPagination pagination)
        {
            var users = await _userRepository
                .GetUsersWhereAsync(x => x.FirstName.ToLower().Contains(phrase.ToLower()) ||
                                         x.LastName.ToLower().Contains(phrase.ToLower()));
            return users.Paginate(pagination);
        }
        
    }    
}    