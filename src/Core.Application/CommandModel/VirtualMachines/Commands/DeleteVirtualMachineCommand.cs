using System;

namespace Core.Application.CommandModel.VirtualMachines.Commands
{
    public class DeleteVirtualMachineCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}