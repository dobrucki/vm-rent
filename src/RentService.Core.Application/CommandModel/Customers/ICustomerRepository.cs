using System;
using System.Threading.Tasks;
using Core.Domain.Customers;

namespace RentService.Core.Application.CommandModel.Customers
{
    public interface ICustomerRepository
    {
        public Task<Customer> GetByIdAsync(Guid id);
        public Task InsertOneAsync(Customer customer);
        public Task UpdateOneAsync(Customer customer);
    }
}