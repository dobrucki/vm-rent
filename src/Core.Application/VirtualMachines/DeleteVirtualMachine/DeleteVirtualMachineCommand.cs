using MediatR;
using System;

namespace Core.Application.VirtualMachines.DeleteVirtualMachine
{
    public class DeleteVirtualMachineCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}