using System;

namespace Core.Application.QueryModel.VirtualMachines.Queries
{
    public sealed class GetVirtualMachineQuery : IQuery<VirtualMachineQueryEntity>
    {
        public Guid VirtualMachineId { get; set; }
    }
}