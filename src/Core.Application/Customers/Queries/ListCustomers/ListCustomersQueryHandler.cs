using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Customers.Queries.ListCustomers
{
    public class ListCustomersQueryHandler : IQueryHandler<ListCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly ILogger<ListCustomersQueryHandler> _logger;
        private readonly ICustomersRepository _customers;

        public ListCustomersQueryHandler(ILogger<ListCustomersQueryHandler> logger, ICustomersRepository customers)
        {
            _logger = logger;
            _customers = customers;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customers.ListCustomersAsync(request.Limit, request.Offset);
            
            var result = customers.Select(customer => new CustomerDto
            {
                Id = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.LastName,
                LastName = customer.LastName
            });
            return result;
        }
    }
}