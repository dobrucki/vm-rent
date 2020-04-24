using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.QueryModel.Rentals.Queries;

namespace Core.Application.QueryModel.Rentals
{
    internal sealed class RentalsQueryHandler : 
        IQueryHandler<GetRentalQuery, RentalQueryEntity>,
        IQueryHandler<ListRentalsQuery, IList<RentalQueryEntity>>
    {
        private readonly IRentalsQueryRepository _rentals;

        public RentalsQueryHandler(IRentalsQueryRepository rentals)
        {
            _rentals = rentals;
        }

        public async Task<RentalQueryEntity> Handle(GetRentalQuery request, CancellationToken cancellationToken)
        {
            return await _rentals.GetRentalByIdAsync(request.RentalId.ToString());
        }

        public async Task<IList<RentalQueryEntity>> Handle(ListRentalsQuery request, CancellationToken cancellationToken)
        {
            return await _rentals.ListRentalsAsync(request.Limit, request.Offset);
        }
    }
}