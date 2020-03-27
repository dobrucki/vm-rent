using System;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class GetVirtualMachineRequest : IRequest<BaseResponseDto<VirtualMachineDto>>
    {
        public Guid Id { get; set; }
    }
}   