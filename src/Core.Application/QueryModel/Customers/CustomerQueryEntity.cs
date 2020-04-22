using System;

namespace Core.Application.QueryModel.Customers
{
    public sealed class CustomerQueryEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}