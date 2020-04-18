using System;
using MediatR;

namespace Core.Application.VirtualMachines.Queries.GetVirtualMachine
{
    public class GetVirtualMachineQuery : IRequest<VirtualMachineDto>
    {
        public Guid VirtualMachineId { get; set; }
    }
}