using Core.Domain.VirtualMachines;

namespace Core.Application.CommandModel.VirtualMachines.Events
{
    public class VirtualMachineDeletedEvent : IEvent
    {
        public VirtualMachine VirtualMachine { get; set; }
    }
}