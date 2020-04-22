using System.Collections.Generic;

namespace Core.Application.QueryModel.Rentals.Queries
{
    public sealed class ListRentalsQuery : IQuery<IList<RentalQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}