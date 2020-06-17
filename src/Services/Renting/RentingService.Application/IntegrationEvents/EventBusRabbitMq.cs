using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RentingService.Application.IntegrationEvents
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private const string BrokerName = "event_bus";
        private readonly IEventBusSubscriptionsManager _subscriptionsManager;
        private readonly ILogger<EventBusRabbitMq> _logger;
        private IModel _consumerChannel;
        private readonly string AUTOFAC_SCOPE_NAME = "event_bus";
        private readonly ILifetimeScope _autofac;

        public EventBusRabbitMq(IEventBusSubscriptionsManager subscriptionsManager, ILogger<EventBusRabbitMq> logger, ILifetimeScope autofac)
        {
            _subscriptionsManager = subscriptionsManager;
            _logger = logger;
            _autofac = autofac;
            _consumerChannel = CreateConsumerChannel();
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
                channel.ExchangeDeclare(exchange: BrokerName, ExchangeType.Direct);
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
            DoInternalSubscription(eventName);
            _subscriptionsManager.AddSubscription<T, TH>();
            _logger.LogCritical($"Subscribe method {eventName}");
            StartBasicConsume();
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
        
        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subscriptionsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
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
                    channel.QueueBind(queue: "xd",
                        exchange: BrokerName,
                        routingKey: eventName);
                }
            }
        }
        
        private void StartBasicConsume()
        {
            if (_consumerChannel != null)
            {
                _logger.LogCritical("Started consuming");
                var consumer = new EventingBasicConsumer(_consumerChannel);
                consumer.Received += async (model, ea) =>
                {
                    
                    var eventName = ea.RoutingKey;
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray().ToArray());

                    _logger.LogWarning($"Received key:{eventName}");
                    _logger.LogWarning($"Msg: {message}");
                    await ProcessEvent(eventName, message);
                    
                    _consumerChannel.BasicAck(ea.DeliveryTag, multiple: false);
                };

                _consumerChannel.BasicConsume(queue: "xd",
                    autoAck: false,
                    consumer: consumer);
            }
            else
            {
                _logger.LogError("StartBasicConsume can not call on _consumerChannelCreated == false");
            }
        }
        
        private IModel CreateConsumerChannel()
        {

            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "rabbitMQ",
                Port = 5672
            };
            var channel = factory.CreateConnection().CreateModel();

            channel.ExchangeDeclare(exchange: BrokerName,
                type: "direct");

            channel.QueueDeclare(queue: "xd",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.CallbackException += (sender, ea) =>
            {
                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }
        
        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogWarning("Processing event");
            if (_subscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                _logger.LogWarning("Processing event 1");
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subscriptionsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        _logger.LogWarning("Sub");
                        var handler = scope.ResolveOptional(subscription.HandlerType);
                        if (handler == null) continue;
                        var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                        _logger.LogWarning($"type: {eventType}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        _logger.LogWarning($"handler: {concreteType}");
                        await (Task) concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                    }
                }
            }
        }
    }
}