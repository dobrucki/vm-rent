using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.CommandModel.Customers;
using Core.Application.CommandModel.Rentals.Commands;
using Core.Application.CommandModel.Rentals.Events;
using Core.Application.CommandModel.VirtualMachines;
using Core.Application.SharedKernel.Exceptions;
using Core.Domain.Rentals;
using MediatR;

namespace Core.Application.CommandModel.Rentals
{
    public class RentalCommandHandler :
        ICommandHandler<CreateRentalCommand>
    {
        private readonly IRentalRepository _rentals;
        private readonly ICustomerRepository _customers;
        private readonly IVirtualMachineRepository _virtualMachines;
        private readonly IMediator _mediator;

        public RentalCommandHandler(IRentalRepository rentals, ICustomerRepository customers, 
            IVirtualMachineRepository virtualMachines, IMediator mediator)
        {
            _rentals = rentals;
            _customers = customers;
            _virtualMachines = virtualMachines;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customers.GetByIdAsync(request.CustomerId);
            var virtualMachine = await _virtualMachines.GetByIdAsync(request.VirtualMachineId);
            var rented = virtualMachine.Rentals
                .Any(x =>
                    x.StartTime < request.EndTime
                    && request.StartTime < x.EndTime);
            if (rented) throw new InvalidCommandException(
                $"Virtual machine with id {request.VirtualMachineId} is not available.");
            var rental = new Rental
            {
                Id = request.Id,
                Customer = customer,
                VirtualMachine = virtualMachine,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };
            await _rentals.InsertOneAsync(rental);
            await _mediator.Publish(new RentalCreatedEvent
            {
                Rental = rental
            }, cancellationToken);
            return Unit.Value;
        }
    }
}