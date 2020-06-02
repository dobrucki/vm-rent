using System;
using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }

        public UserCreatedDomainEvent(Guid userId, string firstName, string lastName, string emailAddress)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}