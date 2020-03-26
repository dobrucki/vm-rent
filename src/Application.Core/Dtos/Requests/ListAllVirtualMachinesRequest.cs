using System.Collections.Generic;
using MediatR;

namespace Application.Core.Dtos.Requests
{
    public class ListAllVirtualMachinesRequest : IRequest<BaseResponseDto<List<VirtualMachineDto>>> 
    {
        
    }
}