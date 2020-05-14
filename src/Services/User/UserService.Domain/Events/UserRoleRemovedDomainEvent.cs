using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class UserRoleRemovedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public UserRole UserRole { get; }
        
        public UserRoleRemovedDomainEvent(User user, UserRole userRole)
        {
            User = user;
            UserRole = userRole;
        }
    }
}