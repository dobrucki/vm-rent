using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.VirtualMachines.EditVirtualMachineDetails
{
    public class EditVirtualMachineDetailsCommandHandler : IRequestHandler<EditVirtualMachineDetailsCommand>
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
            try
            {
                var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.VirtualMachineId);
                virtualMachine.Name = request.Name;
                virtualMachine.ModifiedAt = DateTime.UtcNow;
                await _virtualMachines.UpdateVirtualMachineDetailsAsync(virtualMachine);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw new InvalidRequestException("Could not edit virtual machine.", new[]
                {
                    new PersistenceError(exception.ToString(), exception.Message)
                });
            }
            return Unit.Value;
        }
    }
}