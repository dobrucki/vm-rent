using System;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.RentalAggregate
{
    public class Customer : Entity
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        
        public Customer(Guid id, string firstName, string lastName, string emailAddress) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}