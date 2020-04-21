using System;

namespace Core.Application.QueryModel.Customers.Queries
{
    public class GetCustomerQuery : IQuery<CustomerQueryEntity>
    {
        public Guid CustomerId { get; set; }
    }
}