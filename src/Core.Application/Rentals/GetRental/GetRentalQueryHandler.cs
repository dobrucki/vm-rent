using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Core.Application.Rentals.GetRental
{
    public class GetRentalQueryHandler : IRequestHandler<GetRentalQuery, RentalDto>
    {
        private readonly IRentalsRepository _rentals;

        public GetRentalQueryHandler(IRentalsRepository rentals)
        {
            _rentals = rentals;
        }

        public async Task<RentalDto> Handle(GetRentalQuery request, CancellationToken cancellationToken)
        {
            var rental = await _rentals.GetRentalByIdAsync(request.RentalId);

            var rentalDto = new RentalDto
            {
                Id = rental.Id,
                CustomerId = rental.CustomerId,
                VirtualMachineId = rental.VirtualMachineId,
                StartTime = rental.StartTime,
                EndTime = rental.EndTime
            };
            return rentalDto;
        }
    }
}