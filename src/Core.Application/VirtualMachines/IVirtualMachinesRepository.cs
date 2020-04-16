using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.VirtualMachines;

namespace Core.Application.VirtualMachines
{
    public interface IVirtualMachinesRepository
    {
        Task<VirtualMachine> GetVirtualMachineByIdAsync(Guid virtualMachineId);
        Task UpdateVirtualMachineDetailsAsync(VirtualMachine virtualMachine);
        Task InsertVirtualMachineAsync(VirtualMachine virtualMachine);
        Task DeleteVirtualMachineAsync(VirtualMachine virtualMachine);
        Task<IEnumerable<VirtualMachine>> ListVirtualMachinesAsync(int limit, int offset);
    }
}    