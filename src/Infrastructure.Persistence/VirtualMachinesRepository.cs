using System;
using System.Threading.Tasks;
using Core.Application.VirtualMachines;
using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class VirtualMachinesRepository : IVirtualMachinesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<VirtualMachine> _virtualMachines;

        public VirtualMachinesRepository(ApplicationDbContext context)
        {
            _context = context;
            _virtualMachines = context.Set<VirtualMachine>();
        }

        public async Task<VirtualMachine> GetVirtualMachineByIdAsync(Guid virtualMachineId)
        {
            return await _virtualMachines.FindAsync(virtualMachineId);
        }

        public async Task UpdateVirtualMachineDetailsAsync(VirtualMachine virtualMachine)
        {
            _context.Attach(virtualMachine);
            await _context.SaveChangesAsync();
        }

        public async Task InsertVirtualMachineAsync(VirtualMachine virtualMachine)
        {
            await _virtualMachines.AddAsync(virtualMachine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVirtualMachineAsync(VirtualMachine virtualMachine)
        {
            _virtualMachines.Remove(virtualMachine);
            await _context.SaveChangesAsync();
        }
    }
}