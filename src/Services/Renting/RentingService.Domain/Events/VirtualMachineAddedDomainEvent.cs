using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Events
{
    public class VirtualMachineAddedDomainEvent : IDomainEvent
    {
        public VirtualMachine VirtualMachine { get; set; }
    }
}