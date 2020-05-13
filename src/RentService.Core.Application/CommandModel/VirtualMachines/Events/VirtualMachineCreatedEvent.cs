using Core.Domain.VirtualMachines;

namespace RentService.Core.Application.CommandModel.VirtualMachines.Events
{
    public class VirtualMachineCreatedEvent : IEvent
    {
        public VirtualMachine VirtualMachine { get; set; }
    }
}