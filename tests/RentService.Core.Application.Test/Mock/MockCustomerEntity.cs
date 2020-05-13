using System;

namespace RentService.Core.Application.Test.Mock
{
    public class MockCustomerEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}