using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class UserRoleAddedDomainEvent : IDomainEvent
    {
        public User User { get; }
        public UserRole UserRole { get; }

        public UserRoleAddedDomainEvent(User user, UserRole userRole)
        {
            User = user;
            UserRole = userRole;
        }
    }
}