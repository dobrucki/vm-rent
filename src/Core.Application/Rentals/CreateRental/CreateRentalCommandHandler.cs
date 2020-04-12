using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Application.Customers.GetCustomer;
using Core.Application.SharedKernel;
using Core.Application.VirtualMachines;
using Core.Application.VirtualMachines.GetVirtualMachine;
using Core.Domain.Rentals;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Rentals.CreateRental
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, RentalDto>
    {
        private readonly IRentalsRepository _rentals;
        private readonly ILogger<CreateRentalCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateRentalCommandHandler(IRentalsRepository rentals, ILogger<CreateRentalCommandHandler> logger, 
            IMediator mediator)
        {
            _rentals = rentals;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<RentalDto> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var customerQuery = new GetCustomerQuery
            {
                CustomerId = request.CustomerId
            };
            var virtualMachineQuery = new GetVirtualMachineQuery
            {
                VirtualMachineId = request.VirtualMachineId
            };

            var customer = await _mediator.Send(customerQuery, cancellationToken);
            var virtualMachine = await _mediator.Send(virtualMachineQuery, cancellationToken);

            if (customer is null || virtualMachine is null)
            {
                throw new InvalidRequestException(
                    "Could not create rental.", 
                    new []{new PersistenceError("xd", "xd")});
            }

            var count = (await _rentals
                .GetRentalsAsync(x => 
                    x.VirtualMachineId == request.VirtualMachineId
                    && x.StartTime < request.EndTime
                    && request.StartTime < x.EndTime))
                .Count();
            
            if (count != 0)
            {
                throw new InvalidRequestException(
                    "Could not create rental.", 
                    new []{new PersistenceError("xd1", "xd1")});
            }

            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CustomerId = request.CustomerId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                VirtualMachineId = request.VirtualMachineId
            };

            try
            {
                await _rentals.InsertRentalAsync(rental);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw new InvalidRequestException(
                    "Could not create rental.", 
                    new []{new PersistenceError(exception.ToString(), exception.Message)});
            }
            _logger.LogInformation($"Created rental with id {rental.Id}");
            return new RentalDto
            {
                Id = rental.Id,
                CustomerId = rental.CustomerId,
                VirtualMachineId = rental.VirtualMachineId,
                EndTime = rental.EndTime,
                StartTime = rental.StartTime
            };
        }
    }
}