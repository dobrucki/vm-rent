using System;
using Application.Domain.Dtos;

namespace Application.Domain.Queries.RentalQueries
{
    public class GetRentalByIdQuery : QueryBase<BaseResponseDto<RentalDto>>
    {
        public Guid Id { get; set; }
    }
}