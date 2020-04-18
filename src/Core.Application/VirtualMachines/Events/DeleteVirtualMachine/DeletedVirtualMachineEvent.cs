using System;
using MediatR;

namespace Core.Application.VirtualMachines.Events.DeleteVirtualMachine
{
    public class DeletedVirtualMachineEvent : INotification
    {
        public Guid VirtualMachineId { get; set; }
    }
}