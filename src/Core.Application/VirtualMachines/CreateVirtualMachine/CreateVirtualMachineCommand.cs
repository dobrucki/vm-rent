using MediatR;

namespace Core.Application.VirtualMachines.CreateVirtualMachine
{
    public class CreateVirtualMachineCommand : IRequest<VirtualMachineDto>
    {
        public string Name { get; set; }
    }
}