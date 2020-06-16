using System;
using System.Threading.Tasks;
using RentingService.Application.IntegrationEvents.Events;
using RentingService.Domain.Models.CustomerAggregate;

namespace RentingService.Application.IntegrationEvents.Handlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly ICustomerRepository _customerRepository;

        public UserCreatedIntegrationEventHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            var customer = new Customer(
                id: integrationEvent.UserId,
                firstName: integrationEvent.FirstName,
                lastName: integrationEvent.LastName,
                emailAddress: integrationEvent.EmailAddress);

            await _customerRepository.InsertAsync(customer);
            await _customerRepository.UnitOfWork.CommitAsync();
        }
    }
}