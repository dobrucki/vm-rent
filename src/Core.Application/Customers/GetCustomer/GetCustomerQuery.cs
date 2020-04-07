using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Customers.GetCustomer
{
    public class GetCustomerQuery : IRequest<CustomerDto>
    {
        public Guid CustomerId { get; set; }
    }
}