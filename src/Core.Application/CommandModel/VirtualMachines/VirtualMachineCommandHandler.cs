using System.Threading;
using System.Threading.Tasks;
using Core.Application.CommandModel.VirtualMachines.Commands;
using Core.Application.CommandModel.VirtualMachines.Events;
using Core.Domain.VirtualMachines;
using MediatR;

namespace Core.Application.CommandModel.VirtualMachines
{
    public class VirtualMachineCommandHandler :
        ICommandHandler<CreateVirtualMachineCommand>,
        ICommandHandler<DeleteVirtualMachineCommand>,
        ICommandHandler<EditVirtualMachineDetailsCommand>
    {
        private readonly IVirtualMachineRepository _virtualMachines;
        private readonly IMediator _mediator;

        public VirtualMachineCommandHandler(IVirtualMachineRepository virtualMachines, IMediator mediator)
        {
            _virtualMachines = virtualMachines;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = new VirtualMachine
            {
                Id = request.Id,
                Name = request.Name
            };
            await _virtualMachines.InsertOneAsync(virtualMachine);
            await _mediator.Publish(new VirtualMachineCreatedEvent
            {
                VirtualMachine = virtualMachine
            }, cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = await _virtualMachines.GetByIdAsync(request.Id);
            await _virtualMachines.DeleteOneAsync(virtualMachine);
            await _mediator.Publish(new VirtualMachineDeletedEvent
            {
                VirtualMachine = virtualMachine
            }, cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(EditVirtualMachineDetailsCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = new VirtualMachine
            {
                Id = request.VirtualMachineId,
                Name = request.Name
            };
            await _virtualMachines.UpdateOneAsync(virtualMachine);
            await _mediator.Publish(new VirtualMachineDetailsEditedEvent
            {
                VirtualMachine = virtualMachine
            }, cancellationToken);
            return Unit.Value;
        }
    }
}