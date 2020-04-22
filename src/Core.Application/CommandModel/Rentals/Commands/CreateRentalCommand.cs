using System;

namespace Core.Application.CommandModel.Rentals.Commands
{
    public sealed class CreateRentalCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}