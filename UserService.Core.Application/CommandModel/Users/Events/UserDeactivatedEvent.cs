using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Users.Events
{
    public class UserDeactivatedEvent : IEvent
    {
        public User User { get; set; }
    }
}