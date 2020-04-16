using System.Collections.Generic;
using MediatR;

namespace Core.Application.VirtualMachines.ListVirtualMachines
{
    public class ListVirtualMachinesQuery : IRequest<IEnumerable<VirtualMachineDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}