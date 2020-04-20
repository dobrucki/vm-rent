using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.VirtualMachines.Commands.DeleteVirtualMachine
{
    public class DeleteVirtualMachineCommandHandler : ICommandHandler<DeleteVirtualMachineCommand>
    {
        private readonly IVirtualMachinesRepository _virtualMachines;
        private readonly ILogger<DeleteVirtualMachineCommandHandler> _logger;
        private readonly IMediator _mediator;

        public DeleteVirtualMachineCommandHandler(
            IVirtualMachinesRepository virtualMachines, ILogger<DeleteVirtualMachineCommandHandler> logger, 
            IMediator mediator)
        {
            _virtualMachines = virtualMachines;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.Id);
            await _virtualMachines.DeleteVirtualMachineAsync(virtualMachine);
            _logger.LogInformation($"Deleted virtual machine ({virtualMachine.Id}).");
            return Unit.Value;
        }
    }
}