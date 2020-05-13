using System;

namespace RentService.Core.Application.CommandModel.VirtualMachines.Commands
{
    public class CreateVirtualMachineCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}