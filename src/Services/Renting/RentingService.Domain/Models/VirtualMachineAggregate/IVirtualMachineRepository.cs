using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.VirtualMachineAggregate
{
    public interface IVirtualMachineRepository : IRepository<VirtualMachine>
    {
        Task InsertVirtualMachineAsync(VirtualMachine virtualMachine);
        Task<VirtualMachine> GetVirtualMachineByIdAsync(Guid id);
        Task<IEnumerable<VirtualMachine>> GetVirtualMachinesAsync(int limit, int offset);
        Task UpdateVirtualMachineAsync(VirtualMachine virtualMachine);
    }
}