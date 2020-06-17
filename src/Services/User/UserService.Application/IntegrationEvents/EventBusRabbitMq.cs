using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace UserService.Application.IntegrationEvents
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private const string BrokerName = "event_bus";
        private readonly IEventBusSubscriptionsManager _subscriptionsManager;
        private readonly ILogger<EventBusRabbitMq> _logger;

        public EventBusRabbitMq(IEventBusSubscriptionsManager subscriptionsManager, ILogger<EventBusRabbitMq> logger)
        {
            _subscriptionsManager = subscriptionsManager;
            _logger = logger;
        }

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
                string message = JsonConvert.SerializeObject(integrationEvent);
                _logger.LogInformation(message);
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
            var containsKey = _subscriptionsManager.HasSubscriptionsForEvent(eventName);
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "rabbitMQ",
                Port = 5672
            };
            if (!containsKey)
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: BrokerName, "direct");
                }
            }
            _subscriptionsManager.AddSubscription<T, TH>();
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