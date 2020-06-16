using System;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.CustomerAggregate
{
    public class Customer : Entity, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        
        public Customer(Guid id, string firstName, string lastName, string emailAddress) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }

        public void UpdateDetails(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        
    }
}