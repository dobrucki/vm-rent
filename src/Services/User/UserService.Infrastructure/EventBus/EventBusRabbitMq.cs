using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using UserService.Application.IntegrationEvents;
using RabbitMQ.Client;

namespace UserService.Infrastructure.EventBus
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private const string BrokerName = "event_bus";

        public void Publish(IntegrationEvent integrationEvent)
        {
            var eventName = integrationEvent.GetType().Name;
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "rabbitMQ",
                Port = 5672
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: BrokerName, "direct");
                string message = JsonSerializer.Serialize(integrationEvent);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;
                channel.BasicPublish(exchange: BrokerName, 
                    routingKey: eventName, 
                    mandatory: true,
                    basicProperties: properties, 
                    body: body);
            }
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}