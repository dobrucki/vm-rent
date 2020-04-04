using System;
using Core.Application.Dtos;

namespace Core.Application.Commands.RentalCommands
{
    public class CreateRentalCommand : CommandBase<Result<RentalDto>>
    {
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}