using System;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class DeleteVirtualMachineRequest : IRequest<BaseResponseDto<bool>>
    {
        public Guid Id { get; set; }
    }
}