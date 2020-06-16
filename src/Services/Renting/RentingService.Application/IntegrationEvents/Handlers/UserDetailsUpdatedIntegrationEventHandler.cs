using System;
using System.Threading.Tasks;
using RentingService.Application.IntegrationEvents.Events;
using RentingService.Domain.Models.CustomerAggregate;

namespace RentingService.Application.IntegrationEvents.Handlers
{
    public class UserDetailsUpdatedIntegrationEventHandler 
        : IIntegrationEventHandler<UserDetailsUpdatedIntegrationEvent>
    {
        private readonly ICustomerRepository _customerRepository;

        public UserDetailsUpdatedIntegrationEventHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task Handle(UserDetailsUpdatedIntegrationEvent integrationEvent)
        {
            var customer = await _customerRepository.GetByIdAsync(integrationEvent.UserId);
            customer.UpdateDetails(integrationEvent.FirstName, integrationEvent.LastName);
            await _customerRepository.UnitOfWork.CommitAsync();
        }
    }
}