using MediatR;


namespace Application.Core.Dtos.Requests
{
    public class CreateVirtualMachineRequest : IRequest<BaseResponseDto<VirtualMachineDto>>
    {
        public string Name { get; set; }
    }
}