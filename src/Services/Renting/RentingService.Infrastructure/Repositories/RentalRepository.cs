using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.SeedWork;

namespace RentingService.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentingServiceContext _context;

        public RentalRepository(RentingServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public Task<Rental> GetRentalByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Rental>> GetRentalsWhereAsync(Expression<Func<Rental, bool>> predicate)
        {
            return await _context.Rentals.Where(predicate).ToListAsync();
        }

        public async Task InsertRentalAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
        }
    }
}