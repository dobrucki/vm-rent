using System;
using System.Threading.Tasks;

namespace RentingService.Application.IntegrationEvents
{
    public interface IRentingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent integrationEvent);
    }
}