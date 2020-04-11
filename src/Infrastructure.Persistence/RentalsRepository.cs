using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Application.Rentals;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class RentalsRepository : IRentalsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Rental> _rentals;

        public RentalsRepository(ApplicationDbContext context)
        {
            _context = context;
            _rentals = context.Set<Rental>();
        }

        public async Task<Rental> GetRentalByIdAsync(Guid virtualMachineId)
        {
            return await _rentals.FindAsync(virtualMachineId);
        }    

        public async Task InsertRentalAsync(Rental rental)
        {
            await _rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rental>> ListRentalsAsync(int limit, int offset)
        {
            return await _rentals.Skip(limit * offset).Take(limit).ToListAsync();
        }
    }
}