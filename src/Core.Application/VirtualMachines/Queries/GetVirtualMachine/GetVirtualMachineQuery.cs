using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.VirtualMachines.Queries.GetVirtualMachine
{
    public class GetVirtualMachineQuery : IQuery<VirtualMachineDto>
    {
        public Guid VirtualMachineId { get; set; }
    }
}