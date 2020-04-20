using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.VirtualMachines.Commands.DeleteVirtualMachine
{
    public class DeleteVirtualMachineCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}