using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using Core.Domain.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomersRepository _customers;

        public CreateCustomerCommandHandler(
            ILogger<CreateCustomerCommandHandler> logger, ICustomersRepository customers)
        {
            _logger = logger;
            _customers = customers;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress
            };
            await _customers.InsertCustomerAsync(customer);

            _logger.LogInformation($"Created customer ({customer.Id}).");
            
            return Unit.Value;
        }
    }
}