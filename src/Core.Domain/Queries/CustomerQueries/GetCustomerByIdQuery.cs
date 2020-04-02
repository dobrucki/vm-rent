using System;
using Application.Domain.Dtos;

namespace Application.Domain.Queries.CustomerQueries
{
    public class GetCustomerByIdQuery : QueryBase<BaseResponseDto<CustomerDto>>
    {
        public Guid CustomerId { get; set; }
    }
}