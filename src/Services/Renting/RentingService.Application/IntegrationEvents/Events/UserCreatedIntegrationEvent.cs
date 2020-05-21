using System;

namespace RentingService.Application.IntegrationEvents.Events
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        
        public UserCreatedIntegrationEvent(Guid id, Guid userId) : base(id)
        {
            UserId = userId;
        }
    }
}