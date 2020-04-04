using Core.Application.Dtos;

namespace Core.Application.Commands.VirtualMachineCommands
{
    public class CreateVirtualMachineCommand : CommandBase<Result<VirtualMachineDto>>
    {
        public string Name { get; set; }
    }
}