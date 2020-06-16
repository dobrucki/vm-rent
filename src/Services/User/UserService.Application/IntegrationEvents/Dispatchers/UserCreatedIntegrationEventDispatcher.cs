using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using UserService.Application.IntegrationEvents.Events;
using UserService.Domain.Events;

namespace UserService.Application.IntegrationEvents.Dispatchers
{
    public class UserCreatedIntegrationEventDispatcher : INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<UserCreatedIntegrationEventDispatcher> _logger;

        public UserCreatedIntegrationEventDispatcher(IEventBus eventBus, 
            ILogger<UserCreatedIntegrationEventDispatcher> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = UserCreatedIntegrationEvent.FromUserCreatedDomainEvent(notification);
            _logger.LogCritical("Handled");
            _eventBus.Publish(integrationEvent);
        }
    }
}