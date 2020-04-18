using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.VirtualMachines;
using Core.Domain.VirtualMachines;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class VirtualMachinesRepository : IVirtualMachinesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<VirtualMachineEntity> _virtualMachines;
        private readonly IMapper _mapper;

        public VirtualMachinesRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _virtualMachines = context.Set<VirtualMachineEntity>();
        }

        public async Task<VirtualMachine> GetVirtualMachineByIdAsync(Guid virtualMachineId)
        {
            var virtualMachineEntity = await _virtualMachines
                .SingleOrDefaultAsync(x => x.Id == virtualMachineId);
            return _mapper.Map<VirtualMachine>(virtualMachineEntity);
        }

        public async Task UpdateVirtualMachineDetailsAsync(VirtualMachine virtualMachine)
        {
            _context.Attach(virtualMachine);
            await _context.SaveChangesAsync();
        }

        public async Task InsertVirtualMachineAsync(VirtualMachine virtualMachine)
        {
            var virtualMachineEntity = _mapper.Map<VirtualMachineEntity>(virtualMachine);
            await _virtualMachines.AddAsync(virtualMachineEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVirtualMachineAsync(VirtualMachine virtualMachine)
        {
            var virtualMachineEntity = _mapper.Map<VirtualMachineEntity>(virtualMachine);
            _virtualMachines.Remove(virtualMachineEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VirtualMachine>> ListVirtualMachinesAsync(int limit, int offset)
        {
            return await _virtualMachines
                .Skip(limit * offset)
                .Take(limit)
                .Select(x => _mapper.Map<VirtualMachine>(x))
                .ToListAsync();
        }
    }
}