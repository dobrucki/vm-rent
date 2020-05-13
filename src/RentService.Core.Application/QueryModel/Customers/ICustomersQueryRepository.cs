using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentService.Core.Application.QueryModel.Customers
{
    public interface ICustomersQueryRepository
    {
        Task<CustomerQueryEntity> GetCustomerByIdAsync(Guid customerId);
        Task<IList<CustomerQueryEntity>> ListCustomersAsync(int limit, int offset);
    }
}            