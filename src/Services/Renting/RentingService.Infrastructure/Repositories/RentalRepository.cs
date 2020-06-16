using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RentingService.Application.Queries;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.SeedWork;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentingServiceContext _context;
        private readonly IMapper _mapper;

        public RentalRepository(RentingServiceContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Rental> GetRentalByIdAsync(Guid id)
        {
            var rental = await _context.Rentals
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return _mapper.Map<Rental>(rental);
        }

        public async Task InsertRentalAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
        }

        public Task<IEnumerable<Rental>> GetRentalsAsync(int limit, int offset)
        {
            var entities = _context.Rentals
                .Skip(limit * offset)
                .Take(limit);
            return Task.FromResult(entities.AsEnumerable());
        }
    }
}