using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Events.Customer;
using Application.Domain.Events.Rental;
using Application.Domain.Models;
using Application.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Service.Subscribers
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