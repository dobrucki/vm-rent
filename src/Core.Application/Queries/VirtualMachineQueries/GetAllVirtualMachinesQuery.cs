using System.Collections.Generic;
using Core.Application.Dtos;

namespace Core.Application.Queries.VirtualMachineQueries
{
    public class GetAllVirtualMachinesQuery : 
        QueryBase<Result<IEnumerable<VirtualMachineDto>>>
    {
        
    }
}