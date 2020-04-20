using System.Collections.Generic;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Customers.Queries.ListCustomers
{
    public class ListCustomersQuery : IQuery<IEnumerable<CustomerDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}