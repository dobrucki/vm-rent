using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel.Exceptions;
using MediatR;

namespace Core.Application.Customers.GetCustomer
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly ICustomersRepository _customers;

        public GetCustomerQueryHandler(ICustomersRepository customers)
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

            var customerDto = new CustomerDto
            {
                CustomerId = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
            return customerDto;
        }
    }
}