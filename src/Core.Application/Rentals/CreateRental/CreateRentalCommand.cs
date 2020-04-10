using System;
using MediatR;

namespace Core.Application.Rentals.CreateRental
{
    public class CreateRentalCommand : IRequest<RentalDto>
    {
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}