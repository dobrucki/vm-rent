using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.QueryModel;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.VirtualMachines.Queries.ListVirtualMachines
{
    public class ListVirtualMachinesQueryHandler : IQueryHandler<ListVirtualMachinesQuery, IEnumerable<VirtualMachineDto>>
    {
        private readonly IVirtualMachinesRepository _virtualMachines;

        public ListVirtualMachinesQueryHandler(IVirtualMachinesRepository virtualMachines)
        {
            _virtualMachines = virtualMachines;
        }

        public async Task<IEnumerable<VirtualMachineDto>> Handle(ListVirtualMachinesQuery request, CancellationToken cancellationToken)
        {    
            var virtualMachines = await _virtualMachines.ListVirtualMachinesAsync(request.Limit, request.Offset);
            
            var result = virtualMachines.Select(virtualMachine => new VirtualMachineDto
            {
                Id = virtualMachine.Id,
                Name = virtualMachine.Name
            });
            return result;
        }
    }
}