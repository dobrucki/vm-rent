using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RentService.Core.Application.CommandModel.Rentals;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentService.Infrastructure.Persistence.Entities;

namespace RentService.Infrastructure.Persistence
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<RentalEntity> _rentals;
        private readonly IMapper _mapper;

        public RentalRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _rentals = context.Set<RentalEntity>();
        }

        public async Task InsertOneAsync(Rental rental)
        {
            var rentalEntity = _mapper.Map<RentalEntity>(rental);
            await _rentals.AddAsync(rentalEntity);
            _context.Entry(rentalEntity.VirtualMachine).State = EntityState.Unchanged;
            _context.Entry(rentalEntity.Customer).State = EntityState.Unchanged;
            await _context.SaveChangesAsync();
        }

        public async Task<Rental> GetOneByIdAsync(Guid id)
        {
            var rentalEntity = await _rentals
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
            return _mapper.Map<Rental>(rentalEntity);
        }

        public async Task DeleteOneAsync(Rental rental)
        {
            var rentalEntity = _mapper.Map<RentalEntity>(rental);
            _rentals.Remove(rentalEntity);
            await _context.SaveChangesAsync();
        }
    }
}