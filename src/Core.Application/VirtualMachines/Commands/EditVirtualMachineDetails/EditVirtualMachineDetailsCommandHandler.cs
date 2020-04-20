using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.VirtualMachines.Commands.EditVirtualMachineDetails
{
    public class EditVirtualMachineDetailsCommandHandler : ICommandHandler<EditVirtualMachineDetailsCommand>
    {
        private readonly IVirtualMachinesRepository _virtualMachines;
        private readonly ILogger<EditVirtualMachineDetailsCommandHandler> _logger;

        public EditVirtualMachineDetailsCommandHandler(
            IVirtualMachinesRepository virtualMachines, ILogger<EditVirtualMachineDetailsCommandHandler> logger)
        {
            _virtualMachines = virtualMachines;
            _logger = logger;
        }

        public async Task<Unit> Handle(EditVirtualMachineDetailsCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.VirtualMachineId);
            virtualMachine.Name = request.Name;
            await _virtualMachines.UpdateVirtualMachineDetailsAsync(virtualMachine);
            _logger.LogInformation($"Updated virtual machine ({virtualMachine.Id}).");
            
            return Unit.Value;
        }
    }
}