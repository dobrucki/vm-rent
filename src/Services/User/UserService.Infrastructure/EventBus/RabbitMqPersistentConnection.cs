using System;
using System.IO;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace UserService.Infrastructure.EventBus
{
    public class RabbitMqPersistentConnection : IRabbitMqPersistentConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqPersistentConnection> _logger;
        private bool _disposed;
        private IConnection _connection;
        private const int RetryCount = 3;

        private object sync = new object();


        public RabbitMqPersistentConnection(
            IConnectionFactory connectionFactory, ILogger<RabbitMqPersistentConnection> logger)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            try
            {
                _connection.Dispose();
            }
            catch (IOException exception)
            {
                _logger.LogCritical(exception, exception.Message);
            }
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;
        
        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ Client is trying to connect");

            lock (sync)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(retryCount: RetryCount, retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        _logger.LogWarning(ex,
                            "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})",
                            $"{time.TotalSeconds:n1}", ex.Message);
                    });

                policy.Execute(() => _connection = _connectionFactory.CreateConnection());

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += OnConnectionShutdown;
                    _connection.CallbackException += OnCallbackException;
                    _connection.ConnectionBlocked += OnConnectionBlocked;
                    
                    _logger.LogInformation(
                        "RabbitMQ Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", 
                        _connection.Endpoint.HostName);

                    return true;
                }

                _logger.LogCritical("RabbitMQ connections could not be created");
                return false;
            }
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            if (_disposed) return;
            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to reconnect...");
            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to reconnect...");
            TryConnect();
        }

        void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to reconnect...");
            TryConnect();
        }
    }
}    