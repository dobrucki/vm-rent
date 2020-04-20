using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IQuery<CustomerDto>
    {
        public Guid CustomerId { get; set; }
    }
}