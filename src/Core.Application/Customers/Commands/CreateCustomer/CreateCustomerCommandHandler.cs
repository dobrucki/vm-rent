using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using Core.Application.SharedKernel.Events;
using Core.Domain.Customers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomersRepository _customers;
        private readonly IMediator _mediator;

        public CreateCustomerCommandHandler(
            ILogger<CreateCustomerCommandHandler> logger, ICustomersRepository customers, IMediator mediator)
        {
            _logger = logger;
            _customers = customers;
            _mediator = mediator;
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
            await _mediator.Publish(new CustomerCreatedEvent
            {
                Customer = customer
            }, cancellationToken);

            _logger.LogInformation($"Created customer ({customer.Id}).");
            
            return Unit.Value;
        }
    }
}