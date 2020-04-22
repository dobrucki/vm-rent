using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.QueryModel.VirtualMachines.Queries;

namespace Core.Application.QueryModel.VirtualMachines
{
    internal sealed class VirtualMachineQueryHandler :
        IQueryHandler<GetVirtualMachineQuery, VirtualMachineQueryEntity>,
        IQueryHandler<ListVirtualMachinesQuery, IList<VirtualMachineQueryEntity>>
    {
        public Task<VirtualMachineQueryEntity> Handle(
            GetVirtualMachineQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<VirtualMachineQueryEntity>> Handle(
            ListVirtualMachinesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}