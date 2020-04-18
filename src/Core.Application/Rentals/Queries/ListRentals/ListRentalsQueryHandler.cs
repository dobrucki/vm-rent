using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Rentals.Queries.ListRentals
{
    public class ListRentalsQueryHandler : IRequestHandler<ListRentalsQuery, IEnumerable<RentalDto>>
    {
        private readonly ILogger<ListRentalsQueryHandler> _logger;
        private readonly IRentalsRepository _rentals;

        public ListRentalsQueryHandler(ILogger<ListRentalsQueryHandler> logger, IRentalsRepository rentals)
        {
            _logger = logger;
            _rentals = rentals;
        }

        public async Task<IEnumerable<RentalDto>> Handle(ListRentalsQuery request, CancellationToken cancellationToken)
        {
            var rentals = await _rentals.ListRentalsAsync(request.Limit, request.Offset);
            // // _logger.LogDebug(rentals.ToString());
            // //rentals.ForEach(x => _logger.LogDebug(x.Customer.ToString()));
            // List<RentalDto> rentalDtos = new List<RentalDto>();
            // rentals.ForEach(x => rentalDtos.Add(new RentalDto
            // {
            //     Id = x.Id,
            //     CustomerId = x.Customer.Id,
            //     VirtualMachineId = x.VirtualMachine.Id,
            //     StartTime = x.StartTime,
            //     EndTime = x.EndTime
            // }));
            var result = rentals.Select(rental => new RentalDto
            {
                Id = rental.Id,
                CustomerId = rental.Customer.Id,
                VirtualMachineId = rental.VirtualMachine.Id,
                StartTime = rental.StartTime,
                EndTime = rental.EndTime
            });
            return result;
        }
    }
}