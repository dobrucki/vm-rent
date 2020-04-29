using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.CommandModel.Customers;
using Core.Domain.Customers;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CustomerEntity> _customers;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _customers = context.Set<CustomerEntity>();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var customerEntity = await _customers
                .Include(x => x.Rentals)
                .SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Customer>(customerEntity);
        }

        public async Task InsertOneAsync(Customer customer)
        {
            // var customerEntity = _mapper.Map<CustomerEntity>(customer);
            var customerEntity = new CustomerEntity
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress
            };
            await _customers.AddAsync(customerEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOneAsync(Customer customer)
        {
            var customerEntity = _mapper.Map<CustomerEntity>(customer);
            _customers.Attach(customerEntity);
            await _context.SaveChangesAsync();
        }
    }
}