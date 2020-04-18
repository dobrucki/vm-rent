using System;
using MediatR;

namespace Core.Application.Rentals.Commands.CreateRental
{
    public class CreateRentalCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}