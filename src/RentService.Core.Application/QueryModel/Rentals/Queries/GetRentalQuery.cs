using System;

namespace RentService.Core.Application.QueryModel.Rentals.Queries
{
    public sealed class GetRentalQuery : IQuery<RentalQueryEntity>
    {
        public Guid RentalId { get; set; }
    }
}