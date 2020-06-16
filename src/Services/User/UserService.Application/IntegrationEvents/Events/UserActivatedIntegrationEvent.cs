using System;
using UserService.Domain.Events;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.IntegrationEvents.Events
{
    public class UserActivatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }

        protected UserActivatedIntegrationEvent(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
        }

        public static UserActivatedIntegrationEvent FromUser(User user)
        {
            return new UserActivatedIntegrationEvent(id: Guid.NewGuid(), userId: user.Id);
        }
    }
}