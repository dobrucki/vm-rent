using System;
using Core.Application.SharedKernel;
using MediatR;

namespace Core.Application.Rentals.Commands.CreateRental
{
    public class CreateRentalCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}