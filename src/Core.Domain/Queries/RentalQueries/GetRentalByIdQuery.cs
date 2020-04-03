using System;
using Core.Domain.Dtos;

namespace Core.Domain.Queries.RentalQueries
{
    public class GetRentalByIdQuery : QueryBase<BaseResponseDto<RentalDto>>
    {
        public Guid Id { get; set; }
    }
}