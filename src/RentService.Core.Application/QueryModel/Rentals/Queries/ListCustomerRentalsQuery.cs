using System;
using System.Collections.Generic;

namespace RentService.Core.Application.QueryModel.Rentals.Queries
{
    public class ListCustomerRentalsQuery : IQuery<IList<RentalQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public Guid CustomerId { get; set; }
    }
}