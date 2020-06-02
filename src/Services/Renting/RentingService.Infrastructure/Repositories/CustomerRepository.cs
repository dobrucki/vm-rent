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
            var entity = await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return _mapper.Map<Customer>(entity);
        }

        public async Task InsertAsync(Customer customer)
        {
            var entity = _mapper.Map<CustomerEntity>(customer);
            await _context.Customers.AddAsync(entity);
        }

        public async Task UpdateAsync(Customer customer)
        {
            var entity = await _context.Customers.FindAsync(customer.Id);
            _context.Customers.Update(entity);
        }
    }
}