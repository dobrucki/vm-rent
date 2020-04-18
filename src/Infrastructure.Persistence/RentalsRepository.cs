using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Application.Rentals;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class RentalsRepository : IRentalsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Rental> _rentals;
        private readonly ILogger<RentalsRepository> _logger;

        public RentalsRepository(ApplicationDbContext context, ILogger<RentalsRepository> logger)
        {
            _context = context;
            _logger = logger;
            _rentals = context.Set<Rental>();
        }

        public async Task<Rental> GetRentalByIdAsync(Guid rentalId)
        {
            return await _rentals
                .Include(x => x.Customer)
                .Include(x => x.VirtualMachine)
                .SingleOrDefaultAsync(x => x.Id == rentalId);
        }    

        public async Task InsertRentalAsync(Rental rental)
        {
            await _rentals.AddAsync(rental);
            _context.Entry(rental.VirtualMachine).State = EntityState.Unchanged;
            _context.Entry(rental.Customer).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Rental>> ListRentalsAsync(int limit, int offset)
        {
            return await _rentals
                .Skip(limit * offset)
                .Take(limit)
                .Include(x => x.Customer)
                .Include(x => x.VirtualMachine)
                .ToListAsync();
        }

        public Task<IEnumerable<Rental>> GetRentalsAsync(Expression<Func<Rental, bool>> filter)
        {
            return Task.FromResult(_rentals.Where(filter).AsEnumerable());
        }

        public Task UpdateRangeAsync(IEnumerable<Rental> rentals)
        {
            var rentalsList = rentals.ToList();
            _rentals.AttachRange(rentalsList);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}