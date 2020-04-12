using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.VirtualMachines.DeleteVirtualMachine;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Rentals
{
    public class DeletedVirtualMachineEventHandler : INotificationHandler<DeletedVirtualMachineEvent>
    {
        private readonly IRentalsRepository _rentals;
        private readonly ILogger<DeletedVirtualMachineEventHandler> _logger;

        public DeletedVirtualMachineEventHandler(
            IRentalsRepository rentals, ILogger<DeletedVirtualMachineEventHandler> logger)
        {
            _rentals = rentals;
            _logger = logger;
        }

        public async Task Handle(DeletedVirtualMachineEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Handling event");
            var rentals = (await _rentals
                    .GetRentalsAsync(x => x.VirtualMachineId == notification.VirtualMachineId))
                    .ToList();
            foreach (var rental in rentals)
            {
                rental.VirtualMachineId = Guid.Empty;
            }

            await _rentals.UpdateRangeAsync(rentals);
        }
    }
}