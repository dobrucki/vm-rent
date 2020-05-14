using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Roles.Events
{
    public class RoleCreatedEvent : IEvent
    {
        public Role Role { get; set; }
    }
}
