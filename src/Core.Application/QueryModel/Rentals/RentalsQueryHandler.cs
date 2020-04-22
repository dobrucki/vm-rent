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
        public Task<RentalQueryEntity> Handle(GetRentalQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<RentalQueryEntity>> Handle(ListRentalsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}