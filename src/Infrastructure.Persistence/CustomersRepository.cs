using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Customers;
using Core.Domain.Customers;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CustomerEntity> _customers;
        private readonly IMapper _mapper;

        public CustomersRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _customers = context.Set<CustomerEntity>();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            var customerEntity =  await _customers.SingleOrDefaultAsync(x => x.Id == customerId);
            return _mapper.Map<Customer>(customerEntity);
        }

        public async Task UpdateCustomerDetailsAsync(Customer customer)
        {
            var customerEntity = _mapper.Map<CustomerEntity>(customer);
            _customers.Attach(customerEntity);
            await _context.SaveChangesAsync();
        }

        public async Task InsertCustomerAsync(Customer customer)
        {
            var customerEntity = _mapper.Map<CustomerEntity>(customer);
            await _customers.AddAsync(customerEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> ListCustomersAsync(int limit, int offset)
        {
            return await _customers
                .Skip(limit * offset)
                .Take(limit)
                .Select(x => _mapper.Map<Customer>(x))
                .ToListAsync();
        }
    }
}