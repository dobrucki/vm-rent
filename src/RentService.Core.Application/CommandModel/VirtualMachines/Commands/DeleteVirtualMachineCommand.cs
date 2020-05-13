using System;

namespace RentService.Core.Application.CommandModel.VirtualMachines.Commands
{
    public class DeleteVirtualMachineCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}