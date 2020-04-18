using System;
using MediatR;

namespace Core.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public Guid CustomerId { get; set; }
    }
}