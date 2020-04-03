using System.Threading;
using System.Threading.Tasks;
using Core.Application.Interfaces;
using Core.Domain.Events.RentalEvents;
using Core.Domain.Models;
using Core.Domain.Models.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Subscribers
{
    public class RentalCreatedHandler : INotificationHandler<RentalCreatedEvent>
    {
        private readonly ILogger<CustomerCreatedHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RentalCreatedHandler(
            IUnitOfWork unitOfWork, 
            ILogger<CustomerCreatedHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(
            RentalCreatedEvent notification, CancellationToken cancellationToken = default)
        {
            Rental rental;
            using (_unitOfWork)
            {
                rental = await _unitOfWork.Rentals.GetByIdAsync(notification.RentalId);
            }

            if (rental == null)
            {
                _logger.LogWarning($"Could not find rental with id {notification.RentalId}");
            }
            else
            {
                _logger.LogInformation($"Found rental with id {notification.RentalId}");
            }
        }
    }
}