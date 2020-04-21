using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.QueryModel;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Rentals.Queries.ListRentals
{
    public class ListRentalsQueryHandler : IQueryHandler<ListRentalsQuery, IEnumerable<RentalDto>>
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