using System.Collections.Generic;
using Core.Domain.Dtos;

namespace Core.Domain.Queries.VirtualMachineQueries
{
    public class GetAllVirtualMachinesQuery : 
        QueryBase<BaseResponseDto<IEnumerable<VirtualMachineDto>>>
    {
        
    }
}