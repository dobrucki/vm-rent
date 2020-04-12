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
            try
            {
                var virtualMachine = await _virtualMachines.GetVirtualMachineByIdAsync(request.Id);
                await _virtualMachines.DeleteVirtualMachineAsync(virtualMachine);
                await _mediator.Publish(new DeletedVirtualMachineEvent
                {
                    VirtualMachineId = request.Id
                }, cancellationToken);
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