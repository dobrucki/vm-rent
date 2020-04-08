using System;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Domain.Customers;

namespace Infrastructure.Persistence
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task UpdateCustomerDetailsAsync(Customer customer)
        {
            _context.Customers.Attach(customer);
            await _context.SaveChangesAsync();
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
    }
}