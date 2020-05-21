using System.Threading.Tasks;
using RentingService.Application.IntegrationEvents.Events;

namespace RentingService.Application.IntegrationEvents.Handlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        public Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}