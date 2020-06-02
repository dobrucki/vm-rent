using System;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.IntegrationEvents.Events
{
    public class UserDeactivatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        protected UserDeactivatedIntegrationEvent(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
        }

        public static UserDeactivatedIntegrationEvent FromUser(User user)
        {
            return new UserDeactivatedIntegrationEvent(id: Guid.NewGuid(), userId: user.Id);
        }
    }
}