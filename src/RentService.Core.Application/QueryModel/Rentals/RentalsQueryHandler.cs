using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RentService.Core.Application.QueryModel.VirtualMachines;
using RentService.Core.Application.QueryModel.Rentals.Queries;

namespace RentService.Core.Application.QueryModel.Rentals
{
    internal sealed class RentalsQueryHandler : 
        IQueryHandler<GetRentalQuery, RentalQueryEntity>,
        IQueryHandler<ListRentalsQuery, IList<RentalQueryEntity>>,
        IQueryHandler<ListCustomerRentalsQuery, IList<RentalQueryEntity>>,
        IQueryHandler<ListVirtualMachineRentalsQuery, IList<RentalQueryEntity>>
    {
        private readonly IRentalsQueryRepository _rentals;

        public RentalsQueryHandler(IRentalsQueryRepository rentals)
        {
            _rentals = rentals;
        }

        public async Task<RentalQueryEntity> Handle(
            GetRentalQuery request, CancellationToken cancellationToken)
        {
            return await _rentals.GetRentalByIdAsync(request.RentalId.ToString());
        }

        public async Task<IList<RentalQueryEntity>> Handle(
            ListRentalsQuery request, CancellationToken cancellationToken)
        {
            return await _rentals.ListRentalsAsync(request.Limit, request.Offset);
        }

        public async Task<IList<RentalQueryEntity>> Handle(
            ListCustomerRentalsQuery request, CancellationToken cancellationToken)
        {
            return await _rentals.ListCustomerRentalsAsync(request.Limit, request.Offset, request.CustomerId);
        }

        public async Task<IList<RentalQueryEntity>> Handle(
            ListVirtualMachineRentalsQuery request, CancellationToken cancellationToken)
        {
            return await _rentals.ListVirtualMachineRentalsAsync(
                request.Limit, request.Offset, request.VirtualMachineId);
        }
    }
}