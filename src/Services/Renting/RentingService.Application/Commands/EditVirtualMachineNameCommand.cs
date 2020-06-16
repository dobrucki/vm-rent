using System;

namespace RentingService.Application.Commands
{
    public class EditVirtualMachineNameCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}