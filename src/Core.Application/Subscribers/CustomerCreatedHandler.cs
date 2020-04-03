using System.Threading;
using System.Threading.Tasks;
using Core.Application.Interfaces;
using Core.Domain.Events.CustomerEvents;
using Core.Domain.Models;
using Core.Domain.Models.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Subscribers
{
    public class CustomerCreatedHandler : 
        INotificationHandler<CustomerCreatedEvent>
    {
        private readonly ILogger<CustomerCreatedHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerCreatedHandler(
            IUnitOfWork unitOfWork, 
            ILogger<CustomerCreatedHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(
            CustomerCreatedEvent notification, CancellationToken cancellationToken = default)
        {
            Customer customer;
            using (_unitOfWork)
            {
                customer = await _unitOfWork.Customers.GetByIdAsync(notification.CustomerId);
            }

            if (customer == null)
            {
                _logger.LogWarning($"Could not find customer with id {notification.CustomerId}");
            }
            else
            {
                _logger.LogInformation($"Found customer with id {notification.CustomerId}");
            }
        }
    }
}