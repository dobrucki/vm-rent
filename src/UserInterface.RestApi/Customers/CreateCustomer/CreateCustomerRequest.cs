using System;

namespace UserInterface.RestApi.Customers.CreateCustomer
{
    public class CreateCustomerRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}