using Core.Domain.Dtos;

namespace Core.Domain.Commands.VirtualMachineCommands
{
    public class CreateVirtualMachineCommand : CommandBase<BaseResponseDto<VirtualMachineDto>>
    {
        public string Name { get; set; }
    }
}