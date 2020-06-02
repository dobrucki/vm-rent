using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentingService.Domain.Models.CustomerAggregate;
using RentingService.Domain.Models.RentalAggregate;

namespace RentingService.Application.Commands
{
    public class CreateRentalCommandHandler : ICommandHandler<CreateRentalCommand>
    {
        private readonly IRentalRepository _rentalRepository;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        }

        public async Task<Unit> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer(request.CustomerId, request.CustomerFirstName, request.CustomerLastName, 
                request.CustomerEmailAddress);
            var rental = new Rental(request.Id, customer, request.VirtualMachineId);
            await _rentalRepository.InsertRentalAsync(rental);
            await _rentalRepository.UnitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}