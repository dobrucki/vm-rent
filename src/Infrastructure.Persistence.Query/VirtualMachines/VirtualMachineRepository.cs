using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.QueryModel.VirtualMachines;

namespace Infrastructure.Persistence.Query.VirtualMachines
{
    public class VirtualMachineRepository : IVirtualMachinesQueryRepository
    {
        public Task<VirtualMachineQueryEntity> GetVirtualMachineByIdAsync(Guid virtualMachineId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<VirtualMachineQueryEntity>> ListVirtualMachinesAsync(int limit, int offset)
        {
            throw new NotImplementedException();
        }
    }
}