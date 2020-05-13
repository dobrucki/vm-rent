using Core.Domain.VirtualMachines;

namespace RentService.Core.Application.CommandModel.VirtualMachines.Events
{
    public class VirtualMachineDetailsEditedEvent : IEvent
    {
        public VirtualMachine VirtualMachine { get; set; }
    }
}