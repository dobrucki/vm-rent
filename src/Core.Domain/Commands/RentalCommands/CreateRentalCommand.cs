using System;
using Core.Domain.Dtos;

namespace Core.Domain.Commands.RentalCommands
{
    public class CreateRentalCommand : CommandBase<BaseResponseDto<RentalDto>>
    {
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}