using System;
using MediatR;

namespace Core.Application.VirtualMachines.EditVirtualMachineDetails
{
    public class EditVirtualMachineDetailsCommand : IRequest
    {
        public Guid VirtualMachineId { get; set; }
        public string Name { get; set; }
    }
}