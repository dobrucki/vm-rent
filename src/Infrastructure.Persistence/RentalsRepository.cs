using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Rentals;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class RentalsRepository : IRentalsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<RentalEntity> _rentals;
        private readonly ILogger<RentalsRepository> _logger;
        private readonly IMapper _mapper;

        public RentalsRepository(ApplicationDbContext context, ILogger<RentalsRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _rentals = context.Set<RentalEntity>();
        }

        public async Task<Rental> GetRentalByIdAsync(Guid rentalId)
        {
            var rentalEntity = await _rentals
                .Include(x => x.Customer)
                .Include(x => x.VirtualMachine)
                .SingleOrDefaultAsync(x => x.Id == rentalId);
            return _mapper.Map<Rental>(rentalEntity);
        }    

        public async Task InsertRentalAsync(Rental rental)
        {
            var rentalEntity = _mapper.Map<RentalEntity>(rental);
            await _rentals.AddAsync(rentalEntity);
            _context.Entry(rentalEntity.VirtualMachine).State = EntityState.Unchanged;
            _context.Entry(rentalEntity.Customer).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Rental>> ListRentalsAsync(int limit, int offset)
        {
            return await _rentals
                .Skip(limit * offset)
                .Take(limit)
                .Include(x => x.Customer)
                .Include(x => x.VirtualMachine)
                .Select(x => _mapper.Map<Rental>(x))
                .ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetRentalsAsync(Expression<Func<Rental, bool>> filter)
        {
            // TODO: To change
            var rentalEntities = await _rentals.ToListAsync();
            var rentals = _mapper.Map<IList<Rental>>(rentalEntities).Where(filter.Compile());
            return rentals;
        }

        public Task UpdateRangeAsync(IEnumerable<Rental> rentals)
        {
            var rentalEntities = _mapper.Map<IEnumerable<RentalEntity>>(rentals);
            _rentals.AttachRange(rentalEntities);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }    
    }
}