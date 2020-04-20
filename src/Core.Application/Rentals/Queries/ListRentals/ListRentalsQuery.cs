using System.Collections.Generic;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Rentals.Queries.ListRentals
{
    public class ListRentalsQuery : IQuery<IEnumerable<RentalDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}