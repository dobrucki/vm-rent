using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Customers;

namespace Core.Application.Customers
{
    public interface IReadCustomersRepository
    {
        Task<CustomerDto> GetCustomerByIdAsync(Guid customerId);
        Task<IList<CustomerDto>> ListCustomersAsync(int limit, int offset);
    }
}        