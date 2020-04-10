using MediatR;
using System;

namespace Core.Application.Rentals.GetRental
{
    public class GetRentalQuery : IRequest<RentalDto>
    {
        public Guid RentalId { get; set; }
    }
}