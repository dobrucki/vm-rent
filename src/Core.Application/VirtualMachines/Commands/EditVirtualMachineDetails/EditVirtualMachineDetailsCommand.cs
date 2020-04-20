using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.VirtualMachines.Commands.EditVirtualMachineDetails
{
    public class EditVirtualMachineDetailsCommand : ICommand
    {
        public Guid VirtualMachineId { get; set; }
        public string Name { get; set; }
    }
}