using System;
using System.Threading.Tasks;
using Core.Domain.VirtualMachines;

namespace Core.Application.CommandModel.VirtualMachines
{
    public interface IVirtualMachineRepository
    {
        Task<VirtualMachine> GetByIdAsync(Guid id);
        Task InsertOneAsync(VirtualMachine virtualMachine);
        Task UpdateOneAsync(VirtualMachine virtualMachine);
        Task DeleteOneAsync(VirtualMachine virtualMachine);
    }
}