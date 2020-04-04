using System;
using Core.Application.Dtos;

namespace Core.Application.Queries.CustomerQueries
{
    public class GetCustomerByIdQuery : QueryBase<Result<CustomerDto>>
    {
        public Guid CustomerId { get; set; }
    }
}