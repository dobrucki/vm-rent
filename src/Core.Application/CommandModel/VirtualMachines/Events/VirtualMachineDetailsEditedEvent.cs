using Core.Domain.VirtualMachines;

namespace Core.Application.CommandModel.VirtualMachines.Events
{
    public class VirtualMachineDetailsEditedEvent : IEvent
    {
        public VirtualMachine VirtualMachine { get; set; }
    }
}