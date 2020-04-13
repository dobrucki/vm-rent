using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Application.Customers.GetCustomer;
using Core.Application.SharedKernel;
using Core.Application.SharedKernel.Exceptions;
using Core.Application.VirtualMachines;
using Core.Application.VirtualMachines.GetVirtualMachine;
using Core.Domain.Rentals;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Rentals.CreateRental
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand>
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

        public async Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
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

            Expression<Func<Rental, bool>> filter = x =>
                x.VirtualMachineId == request.VirtualMachineId
                && x.StartTime < request.EndTime
                && request.StartTime < x.EndTime;

            var rentals = await _rentals.GetRentalsAsync(filter);
            
            if (rentals.Any())
            {
                var message =
                    $"Creating rental ({request.Id}) " +
                    $"failed because virtual machine ({request.VirtualMachineId}) is already rented.";
                throw new InvalidCommandException(message);
            }

            var rental = new Rental
            {
                Id = request.CustomerId,
                CustomerId = request.CustomerId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                VirtualMachineId = request.VirtualMachineId
            };

            await _rentals.InsertRentalAsync(rental);
            
            _logger.LogInformation($"Created rental ({rental.Id})");
            return Unit.Value;
        }
    }
}