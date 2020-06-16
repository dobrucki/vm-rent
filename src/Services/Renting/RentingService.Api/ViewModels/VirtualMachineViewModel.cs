using System;

namespace RentingService.Api.ViewModels
{
    public class VirtualMachineViewModel
    {
        public VirtualMachineViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}