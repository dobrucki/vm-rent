using System;
using MediatR;

namespace Core.Application.VirtualMachines.Commands.CreateVirtualMachine
{
    public class CreateVirtualMachineCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}