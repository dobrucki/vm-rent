using System;
using MediatR;

namespace Core.Application.Rentals.Queries.GetRental
{
    public class GetRentalQuery : IRequest<RentalDto>
    {
        public Guid RentalId { get; set; }
    }
}