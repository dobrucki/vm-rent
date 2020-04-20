using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.VirtualMachines.Commands.CreateVirtualMachine
{
    public class CreateVirtualMachineCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}