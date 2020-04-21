using System.Collections.Generic;
using Core.Application.QueryModel;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.VirtualMachines.Queries.ListVirtualMachines
{
    public class ListVirtualMachinesQuery : IQuery<IEnumerable<VirtualMachineDto>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}