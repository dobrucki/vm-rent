using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using Core.Application.SharedKernel.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.VirtualMachines.GetVirtualMachine
{
    public class GetVirtualMachineQueryHandler : IRequestHandler<GetVirtualMachineQuery, VirtualMachineDto>
    {
        private readonly IVirtualMachinesRepository _virtualMachines;
    
        public GetVirtualMachineQueryHandler(
            IVirtualMachinesRepository virtualMachines)
        {
            _virtualMachines = virtualMachines;
        }

        public async Task<VirtualMachineDto> Handle(GetVirtualMachineQuery request, CancellationToken cancellationToken)
        {
            var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.VirtualMachineId);
            if (virtualMachine is null)
            {
                throw new NotFoundException("Virtual machine", request.VirtualMachineId);
            }

            return new VirtualMachineDto
            {
                Name = virtualMachine.Name,
                Id = virtualMachine.Id
            };
        }
    }
}