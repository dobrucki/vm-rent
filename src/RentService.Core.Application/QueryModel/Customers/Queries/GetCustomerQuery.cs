using System;

namespace RentService.Core.Application.QueryModel.Customers.Queries
{
    public sealed class GetCustomerQuery : IQuery<CustomerQueryEntity>
    {
        public Guid CustomerId { get; set; }
    }
}