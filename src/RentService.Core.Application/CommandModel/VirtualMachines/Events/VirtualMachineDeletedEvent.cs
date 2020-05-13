using Core.Domain.VirtualMachines;

namespace RentService.Core.Application.CommandModel.VirtualMachines.Events
{
    public class VirtualMachineDeletedEvent : IEvent
    {
        public VirtualMachine VirtualMachine { get; set; }
    }
}