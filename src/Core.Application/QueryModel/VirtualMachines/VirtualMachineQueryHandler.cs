using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.QueryModel.VirtualMachines.Queries;
using Core.Application.SharedKernel.Exceptions;

namespace Core.Application.QueryModel.VirtualMachines
{
    internal sealed class VirtualMachineQueryHandler :
        IQueryHandler<GetVirtualMachineQuery, VirtualMachineQueryEntity>,
        IQueryHandler<ListVirtualMachinesQuery, IList<VirtualMachineQueryEntity>>
    {
        private readonly IVirtualMachinesQueryRepository _virtualMachines;

        public VirtualMachineQueryHandler(IVirtualMachinesQueryRepository virtualMachines)
        {
            _virtualMachines = virtualMachines;
        }

        public async Task<VirtualMachineQueryEntity> Handle(
            GetVirtualMachineQuery request, CancellationToken cancellationToken)
        {
            var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.VirtualMachineId);
            return virtualMachine ?? throw new NotFoundException("Customer", request.VirtualMachineId);
        }

        public async Task<IList<VirtualMachineQueryEntity>> Handle(
            ListVirtualMachinesQuery request, CancellationToken cancellationToken)
        {
            return await _virtualMachines.ListVirtualMachinesAsync(request.Limit, request.Offset) 
                   ?? new List<VirtualMachineQueryEntity>(0);
        }
    }
}