using System.Collections.Generic;

namespace RentService.Core.Application.QueryModel.Customers.Queries
{
    public sealed class ListCustomersQuery : IQuery<IList<CustomerQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}