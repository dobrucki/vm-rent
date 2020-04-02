using System;
using Application.Domain.Dtos;

namespace Application.Domain.Commands.Rental
{
    public class CreateRentalCommand : CommandBase<BaseResponseDto<RentalDto>>
    {
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}