using System;
using MediatR;

namespace Core.Application.Customers.EditCustomerDetails
{
    public class EditCustomerDetailsCommand : IRequest
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}