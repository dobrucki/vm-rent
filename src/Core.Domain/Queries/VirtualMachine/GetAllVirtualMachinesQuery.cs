using System.Collections.Generic;

namespace Application.Domain.Queries.VirtualMachine
{
    using Dtos;
    
    public class GetAllVirtualMachinesQuery : 
        QueryBase<BaseResponseDto<IEnumerable<VirtualMachineDto>>>
    {
        
    }
}