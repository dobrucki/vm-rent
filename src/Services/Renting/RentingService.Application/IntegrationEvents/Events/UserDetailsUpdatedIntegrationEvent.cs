using System;

namespace RentingService.Application.IntegrationEvents.Events
{
    public class UserDetailsUpdatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        
        public UserDetailsUpdatedIntegrationEvent(
            Guid id, Guid userId, string firstName, string lastName) : base(id)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}