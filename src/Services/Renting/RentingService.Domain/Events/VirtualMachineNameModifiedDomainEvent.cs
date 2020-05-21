using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Events
{
    public class VirtualMachineNameModifiedDomainEvent : IDomainEvent
    {
        public VirtualMachine VirtualMachine { get; set; }
    }
}