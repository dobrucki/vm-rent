using System;
using Core.Application.Dtos;

namespace Core.Application.Queries.RentalQueries
{
    public class GetRentalByIdQuery : QueryBase<Result<RentalDto>>
    {
        public Guid Id { get; set; }
    }
}