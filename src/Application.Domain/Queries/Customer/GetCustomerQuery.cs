using System;
using Application.Domain.Commands;
using Application.Domain.Dtos;

namespace Application.Domain.Queries.Customer
{
    public class GetCustomerQuery : QueryBase<BaseResponseDto<CustomerDto>>
    {
        public Guid CustomerId { get; set; }
    }
}