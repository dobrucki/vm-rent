using System.Collections.Generic;
using Core.Domain.Rentals;
using Core.Domain.SharedKernel;

namespace Core.Domain.Customers
{
    public class Customer : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public IList<Rental> Rentals { get; set; }
    }
}