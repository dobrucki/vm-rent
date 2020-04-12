using System;
using MediatR;

namespace Core.Application.VirtualMachines.DeleteVirtualMachine
{
    public class DeletedVirtualMachineEvent : INotification
    {
        public Guid VirtualMachineId { get; set; }
    }
}