using System;
using System.Threading.Tasks;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByIdAsync(Guid id);
        Task InsertAsync(Customer customer);
        Task UpdateAsync(Customer customer);
    }
}    