using System.Collections.Generic;
using Application.Domain.Dtos;

namespace Application.Domain.Queries.VirtualMachineQueries
{
    public class GetAllVirtualMachinesQuery : 
        QueryBase<BaseResponseDto<IEnumerable<VirtualMachineDto>>>
    {
        
    }
}