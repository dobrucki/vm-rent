using System;

namespace Core.Application.QueryModel.Customers.Queries
{
    public sealed class GetCustomerQuery : IQuery<CustomerQueryEntity>
    {
        public Guid CustomerId { get; set; }
    }
}