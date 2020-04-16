using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomersRepository(ApplicationDbContext context)
        {
            _context = context;
            _customers = context.Set<Customer>();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            return await _customers.SingleOrDefaultAsync(x => x.Id == customerId);
        }

        public async Task UpdateCustomerDetailsAsync(Customer customer)
        {
            _customers.Attach(customer);
            await _context.SaveChangesAsync();
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            await _customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> ListCustomersAsync(int limit, int offset)
        {
            return await _customers
                .Skip(limit * offset)
                .Take(limit)
                .ToListAsync();
        }
    }
}