using UserService.Domain.Models.RoleAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class RoleNameUpdatedDomainEvent : IDomainEvent
    {
        public Role Role { get; }
        public string RoleName { get; }

        public RoleNameUpdatedDomainEvent(Role role, string roleName)
        {
            Role = role;
            RoleName = roleName;
        }
    }
}