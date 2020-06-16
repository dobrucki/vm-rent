using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentingService.Domain.Models.CustomerAggregate;
using RentingService.Domain.SeedWork;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RentingServiceContext _context;
        private readonly IMapper _mapper;
        
        public CustomerRepository(RentingServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork => _context;
        
        public async Task<Customer> GetByIdAsync(Guid id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return customer;
        }

        public async Task InsertAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public Task UpdateAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}