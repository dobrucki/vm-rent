using System.Collections.Generic;

namespace Core.Application.QueryModel.VirtualMachines.Queries
{
    public sealed class ListVirtualMachinesQuery : IQuery<IList<VirtualMachineQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}