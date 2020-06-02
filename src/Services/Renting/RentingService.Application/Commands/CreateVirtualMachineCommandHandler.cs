using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentingService.Domain.Models.VirtualMachineAggregate;

namespace RentingService.Application.Commands
{
    public class CreateVirtualMachineCommandHandler : ICommandHandler<CreateVirtualMachineCommand>
    {
        private readonly IVirtualMachineRepository _virtualMachineRepository;

        public CreateVirtualMachineCommandHandler(IVirtualMachineRepository virtualMachineRepository)
        {
            _virtualMachineRepository = virtualMachineRepository ??
                                        throw new ArgumentNullException(nameof(virtualMachineRepository));
        }

        public async Task<Unit> Handle(CreateVirtualMachineCommand request, CancellationToken cancellationToken)
        {
            var virtualMachine = new VirtualMachine(request.Id, request.Name);
            await _virtualMachineRepository.InsertVirtualMachineAsync(virtualMachine);
            await _virtualMachineRepository.UnitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}