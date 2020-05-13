using System;

namespace RentService.Core.Application.CommandModel.VirtualMachines.Commands
{
    public class EditVirtualMachineDetailsCommand : ICommand
    {
        public Guid VirtualMachineId { get; set; }
        public string Name { get; set; }
    }
}