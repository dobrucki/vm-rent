using System;
using UserService.Application.IntegrationEvents;
using RabbitMQ.Client;

namespace UserService.Infrastructure.EventBus
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        
        public void Publish(IntegrationEvent integrationEvent)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}