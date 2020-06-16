using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentingService.Domain.Models.VirtualMachineAggregate;

namespace RentingService.Application.Commands
{
    public class EditVirtualMachineNameCommandHandler : ICommandHandler<EditVirtualMachineNameCommand>
    {
        private readonly IVirtualMachineRepository _virtualMachineRepository;

        public EditVirtualMachineNameCommandHandler(IVirtualMachineRepository virtualMachineRepository)
        {
            _virtualMachineRepository = virtualMachineRepository;
        }

        public async Task<Unit> Handle(EditVirtualMachineNameCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = await _virtualMachineRepository.GetVirtualMachineByIdAsync(request.Id);
            virtualMachine.Name = request.Name;
            await _virtualMachineRepository.UpdateVirtualMachineAsync(virtualMachine);
            await _virtualMachineRepository.UnitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}