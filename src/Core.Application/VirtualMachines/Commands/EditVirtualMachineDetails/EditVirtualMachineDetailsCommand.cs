using System;
using MediatR;

namespace Core.Application.VirtualMachines.Commands.EditVirtualMachineDetails
{
    public class EditVirtualMachineDetailsCommand : IRequest
    {
        public Guid VirtualMachineId { get; set; }
        public string Name { get; set; }
    }
}