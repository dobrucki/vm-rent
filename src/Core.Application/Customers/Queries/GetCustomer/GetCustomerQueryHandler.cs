using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using Core.Application.SharedKernel.Exceptions;
using MediatR;

namespace Core.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IReadCustomersRepository _customers;

        public GetCustomerQueryHandler(IReadCustomersRepository customers)
        {
            _customers = customers;
        }
        
        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customers.GetCustomerByIdAsync(request.CustomerId);
            if (customer is null)
            {
                throw new NotFoundException("customer", request.CustomerId);
            }
            return customer;
        }
    }
}