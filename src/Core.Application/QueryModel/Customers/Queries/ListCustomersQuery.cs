using System.Collections.Generic;

namespace Core.Application.QueryModel.Customers.Queries
{
    public class ListCustomersQuery : IQuery<IList<CustomerQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}