using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public User User { get; }

        public UserCreatedDomainEvent(User user)
        {
            User = user;
        }
    }
}