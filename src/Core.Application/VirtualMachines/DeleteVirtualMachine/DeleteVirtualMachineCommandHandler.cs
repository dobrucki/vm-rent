using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.VirtualMachines.DeleteVirtualMachine
{
    public class DeleteVirtualMachineCommandHandler : IRequestHandler<DeleteVirtualMachineCommand>
    {
        private readonly IVirtualMachinesRepository _virtualMachines;
        private readonly ILogger<DeleteVirtualMachineCommandHandler> _logger;

        public DeleteVirtualMachineCommandHandler(
            IVirtualMachinesRepository virtualMachines, ILogger<DeleteVirtualMachineCommandHandler> logger)
        {
            _virtualMachines = virtualMachines;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.Id);
                await _virtualMachines.DeleteVirtualMachineAsync(virtualMachine);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw new InvalidRequestException(
                    "Could not create virtual machine.", 
                    new []{new PersistenceError(exception.ToString(), exception.Message)});
            }
            return Unit.Value;
        }
    }
}