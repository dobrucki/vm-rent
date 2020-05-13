using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentService.Core.Application.QueryModel.VirtualMachines
{
    public interface IVirtualMachinesQueryRepository
    {
        Task<VirtualMachineQueryEntity> GetVirtualMachineByIdAsync(Guid virtualMachineId);
        Task<IList<VirtualMachineQueryEntity>> ListVirtualMachinesAsync(int limit, int offset);
    }
}