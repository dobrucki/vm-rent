using System.Collections.Generic;
using MediatR;

namespace Core.Application.Customers.Queries.ListCustomers
{
    public class ListCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}