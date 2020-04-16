using System.Collections.Generic;
using MediatR;

namespace Core.Application.Customers.ListCustomers
{
    public class ListCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
        
    }
}