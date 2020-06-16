using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Domain.SeedWork;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.Repositories
{
    public class VirtualMachineRepository : IVirtualMachineRepository
    {
        private readonly RentingServiceContext _context;
        private readonly IMapper _mapper;
        
        public VirtualMachineRepository(RentingServiceContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork => _context;


        public async Task InsertVirtualMachineAsync(VirtualMachine virtualMachine)
        {
            await _context.VirtualMachines.AddAsync(virtualMachine);
        }

        public async Task<VirtualMachine> GetVirtualMachineByIdAsync(Guid id)
        {
            var virtualMachine = await _context.VirtualMachines
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return virtualMachine;
        }

        public async Task<IEnumerable<VirtualMachine>> GetVirtualMachinesAsync(int limit, int offset)
        {
            var virtualMachines = await _context.VirtualMachines
                .AsNoTracking()
                .Skip(limit * offset)
                .Take(limit)
                .ToListAsync();
            return virtualMachines;
        }

        public Task UpdateVirtualMachineAsync(VirtualMachine virtualMachine)
        {
            _context.Entry(virtualMachine).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}