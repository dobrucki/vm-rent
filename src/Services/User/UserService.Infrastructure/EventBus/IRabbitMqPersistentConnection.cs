using System;

namespace UserService.Infrastructure.EventBus
{
    public interface IRabbitMqPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        
    }
}