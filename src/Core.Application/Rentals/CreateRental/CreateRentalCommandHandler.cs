using System;
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
        private readonly IVirtualMachinesRepository _virtualMachines;
        private readonly ICustomersRepository _customers;
        private readonly ILogger<CreateRentalCommandHandler> _logger;
        private readonly IMediator _mediator;

        public CreateRentalCommandHandler(IRentalsRepository rentals, ILogger<CreateRentalCommandHandler> logger, 
            IMediator mediator, IVirtualMachinesRepository virtualMachines, ICustomersRepository customers)
        {
            _rentals = rentals;
            _logger = logger;
            _mediator = mediator;
            _virtualMachines = virtualMachines;
            _customers = customers;
        }

        public async Task<RentalDto> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customers.GetCustomerByIdAsync(request.CustomerId);
            var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.VirtualMachineId);
            var rental = new Rental
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Customer = customer,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                VirtualMachine = virtualMachine
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
                CustomerId = rental.Customer.Id,
                VirtualMachineId = rental.VirtualMachine.Id,
                EndTime = rental.EndTime,
                StartTime = rental.StartTime
            };
        }
    }
}