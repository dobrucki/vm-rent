using System;
using UserService.Domain.Events;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Application.IntegrationEvents.Events
{
    public class UserCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        
        protected UserCreatedIntegrationEvent(
            Guid id, Guid userId, string firstName, string lastName, string emailAddress) : base(id)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public static UserCreatedIntegrationEvent FromUser(User user)
        {
            return new UserCreatedIntegrationEvent(
                id: Guid.NewGuid(), userId: user.Id, firstName: user.FirstName, 
                lastName: user.LastName, emailAddress: user.EmailAddress);
        }

        public static UserCreatedIntegrationEvent FromUserCreatedDomainEvent(UserCreatedDomainEvent domainEvent)
        {
            return new UserCreatedIntegrationEvent(
                id: Guid.NewGuid(), domainEvent.UserId, domainEvent.FirstName, 
                domainEvent.LastName, domainEvent.EmailAddress);
        }
    }
}