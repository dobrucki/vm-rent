using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class UserRoleAddedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public Role Role { get; }

        public UserRoleAddedDomainEvent(User user, Role role)
        {
            User = user;
            Role = role;
        }
    }
}