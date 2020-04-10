using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Application.SharedKernel;
using Core.Domain.VirtualMachines;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.VirtualMachines.CreateVirtualMachine
{
    public class CreateVirtualMachineCommandHandler : IRequestHandler<CreateVirtualMachineCommand, VirtualMachineDto>
    {
        private readonly IVirtualMachinesRepository _virtualMachines;
        private readonly ILogger<CreateVirtualMachineCommandHandler> _logger;

        public CreateVirtualMachineCommandHandler(
            IVirtualMachinesRepository virtualMachines, ILogger<CreateVirtualMachineCommandHandler> logger)
        {
            _virtualMachines = virtualMachines;
            _logger = logger;
        }

        public async Task<VirtualMachineDto> Handle(CreateVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = new VirtualMachine
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = null,

                Name = request.Name
            };
            try
            {
                await _virtualMachines.InsertVirtualMachineAsync(virtualMachine);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw new InvalidRequestException(
                    "Could not create virtual machine.", 
                    new []{new PersistenceError(exception.ToString(), exception.Message)});
            }

            _logger.LogInformation($"Created virtual machine with id {virtualMachine.Id}.");
            return new VirtualMachineDto
            {
                Id = virtualMachine.Id,
                Name = virtualMachine.Name
            };
        }
    }
}