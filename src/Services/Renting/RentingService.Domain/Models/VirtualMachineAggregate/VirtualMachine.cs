using System;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.VirtualMachineAggregate
{
    public class VirtualMachine : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        
        public VirtualMachine(Guid id, string name) : base(id)
        {
            Name = name;
        }
    }
}    