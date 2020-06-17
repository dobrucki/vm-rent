using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentingService.Application.IntegrationEvents.Events;
using RentingService.Domain.Models.CustomerAggregate;

namespace RentingService.Application.IntegrationEvents.Handlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;

        public UserCreatedIntegrationEventHandler(ICustomerRepository customerRepository, ILogger<UserCreatedIntegrationEventHandler> logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _logger = logger;
        }

        public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            var customer = new Customer(
                id: integrationEvent.UserId,
                firstName: integrationEvent.FirstName,
                lastName: integrationEvent.LastName,
                emailAddress: integrationEvent.EmailAddress);

            _logger.LogCritical("Handled integration");
            await _customerRepository.InsertAsync(customer);
            await _customerRepository.UnitOfWork.CommitAsync();
        }
    }
}