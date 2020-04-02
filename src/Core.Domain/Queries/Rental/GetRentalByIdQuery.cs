using System;
using Application.Domain.Dtos;

namespace Application.Domain.Queries.Rental
{
    public class GetRentalByIdQuery : QueryBase<BaseResponseDto<RentalDto>>
    {
        public Guid Id { get; set; }
    }
}