using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using Core.Domain.VirtualMachines;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.VirtualMachines.Commands.CreateVirtualMachine
{
    public class CreateVirtualMachineCommandHandler : ICommandHandler<CreateVirtualMachineCommand>
    {
        private readonly IVirtualMachinesRepository _virtualMachines;
        private readonly ILogger<CreateVirtualMachineCommandHandler> _logger;

        public CreateVirtualMachineCommandHandler(
            IVirtualMachinesRepository virtualMachines, ILogger<CreateVirtualMachineCommandHandler> logger)
        {
            _virtualMachines = virtualMachines;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = new VirtualMachine
            {
                Id = request.Id,
                Name = request.Name
            };
            
            await _virtualMachines.InsertVirtualMachineAsync(virtualMachine);

            _logger.LogInformation($"Created virtual machine ({virtualMachine.Id}).");
            return Unit.Value;
        }
    }
}