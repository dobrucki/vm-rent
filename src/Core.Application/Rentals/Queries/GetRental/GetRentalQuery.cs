using System;
using Core.Application.QueryModel;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Rentals.Queries.GetRental
{
    public class GetRentalQuery : IQuery<RentalDto>
    {
        public Guid RentalId { get; set; }
    }
}