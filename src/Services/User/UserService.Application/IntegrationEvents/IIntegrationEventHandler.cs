using System.Threading.Tasks;

namespace UserService.Application.IntegrationEvents
{
    public interface IIntegrationEventHandler<in T> : IIntegrationEventHandler
        where T : IntegrationEvent
    {
        Task Handle(T integrationEvent);
    }

    public interface IIntegrationEventHandler
    { }
}