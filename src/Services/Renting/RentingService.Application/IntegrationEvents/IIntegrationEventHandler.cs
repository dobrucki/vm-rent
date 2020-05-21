using System.Threading.Tasks;

namespace RentingService.Application.IntegrationEvents
{
    public interface IIntegrationEventHandler<in T> : IIntegrationEventHandler
        where T : IntegrationEvent
    {
        Task Handle(T integrationEvent);
    }

    public interface IIntegrationEventHandler
    { }
}