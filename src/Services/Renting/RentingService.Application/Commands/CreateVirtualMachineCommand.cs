using System;

namespace RentingService.Application.Commands
{
    public class CreateVirtualMachineCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}