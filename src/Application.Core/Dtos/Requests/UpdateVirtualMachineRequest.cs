using System;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class UpdateVirtualMachineRequest : IRequest<BaseResponseDto<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}   