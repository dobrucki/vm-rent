using System;
using System.Threading.Tasks;
using Core.Domain.VirtualMachines;

namespace Core.Application.VirtualMachines
{
    public interface IVirtualMachinesRepository
    {
        Task<VirtualMachine> GetVirtualMachineById(Guid virtualMachineId);
    }
}