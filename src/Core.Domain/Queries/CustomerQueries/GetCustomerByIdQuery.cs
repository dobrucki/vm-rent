using System;
using Core.Domain.Dtos;

namespace Core.Domain.Queries.CustomerQueries
{
    public class GetCustomerByIdQuery : QueryBase<BaseResponseDto<CustomerDto>>
    {
        public Guid CustomerId { get; set; }
    }
}