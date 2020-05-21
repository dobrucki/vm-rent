using System;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.RentalAggregate
{
    public class Customer : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        
        public Customer(Guid id, string firstName, string lastName, string emailAddress) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}