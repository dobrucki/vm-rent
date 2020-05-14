using UserService.Core.Domain;

namespace UserService.Core.Application.CommandModel.Users.Events
{
    public class UserActivatedEvent : IEvent
    {
        public User User { get; set; }
    }
}