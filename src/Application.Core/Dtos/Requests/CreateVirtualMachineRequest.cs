using MediatR;


namespace Application.Core.Dtos.Requests
{
    public class CreateVirtualMachineRequest : IRequest<BaseResponseDto<bool>>
    {
        public string Name { get; set; }
    }
}