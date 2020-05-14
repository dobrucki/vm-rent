using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Users.Events
{
    public class UserCreatedEvent : IEvent
    {
        public User User { get; set; }
    }
}