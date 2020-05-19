using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class UserRoleRemovedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public Role Role { get; }
        
        public UserRoleRemovedDomainEvent(User user, Role role)
        {
            User = user;
            Role = role;
        }
    }
}