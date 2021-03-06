using System;

namespace RentingService.Application.IntegrationEvents.Events
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        
        public UserCreatedIntegrationEvent(
            Guid id, Guid userId, string firstName, string lastName, string emailAddress) : base(id)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}