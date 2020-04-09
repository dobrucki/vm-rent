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

        public async Task<VirtualMachine> GetVirtualMachineById(Guid virtualMachineId)
        {
            return await _virtualMachines.FindAsync(virtualMachineId);
        }
    }
}