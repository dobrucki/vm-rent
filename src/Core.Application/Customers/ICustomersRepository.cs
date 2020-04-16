using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Customers;

namespace Core.Application.Customers
{
    public interface ICustomersRepository
    {
        Task<Customer> GetCustomerByIdAsync(Guid customerId);
        Task UpdateCustomerDetailsAsync(Customer customer);
        Task InsertCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> ListCustomersAsync(int limit, int offset);
    }
}