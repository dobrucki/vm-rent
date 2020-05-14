using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class UserPasswordUpdated : IDomainEvent
    {
        public User User { get; }
        public string UserPassword { get; }

        public UserPasswordUpdated(User user, string userPassword)
        {
            User = user;
            UserPassword = userPassword;
        }
    }
}