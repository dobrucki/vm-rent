using System.Collections.Generic;
using MediatR;

namespace Core.Application.Rentals.ListRentals
{
    public class ListRentalsQuery : IRequest<IEnumerable<RentalDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}