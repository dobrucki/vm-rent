using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Customers.Queries.GetCustomer;
using Core.Application.SharedKernel;
using Core.Application.SharedKernel.Exceptions;
using Core.Application.VirtualMachines.Queries.GetVirtualMachine;
using Core.Domain.Customers;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Rentals.Commands.CreateRental
{
    public class CreateRentalCommandHandler : ICommandHandler<CreateRentalCommand>
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
                x.VirtualMachine.Id == request.VirtualMachineId
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
                Id = request.Id,
                Customer = new Customer
                {
                    Id = customer.Id,
                    EmailAddress = customer.EmailAddress,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                },
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                VirtualMachine = new VirtualMachine
                {
                    Id = virtualMachine.Id,
                    Name = virtualMachine.Name
                }
            };

            await _rentals.InsertRentalAsync(rental);
            
            _logger.LogInformation($"Created rental ({rental.Id})");
            return Unit.Value;
        }
    }
}