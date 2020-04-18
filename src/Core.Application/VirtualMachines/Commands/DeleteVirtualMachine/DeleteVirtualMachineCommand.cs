using System;
using MediatR;

namespace Core.Application.VirtualMachines.Commands.DeleteVirtualMachine
{
    public class DeleteVirtualMachineCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}