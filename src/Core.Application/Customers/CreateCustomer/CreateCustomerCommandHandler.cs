using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using Core.Domain.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Customers.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomersRepository _customers;

        public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, ICustomersRepository customers)
        {
            _logger = logger;
            _customers = customers;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = null,

                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress
            };
            try
            {
                await _customers.InsertCustomerAsync(customer);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw new InvalidRequestException(
                    "Could not create customer.", 
                    new []{new PersistenceError(exception.ToString(), exception.Message)});
            }

            _logger.LogInformation($"Created customer with id {customer.Id}.");
            return new CustomerDto
            {
                CustomerId = customer.Id,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };
        }
    }
}