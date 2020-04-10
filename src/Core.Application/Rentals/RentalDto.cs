using System;

namespace Core.Application.Rentals
{
    public class RentalDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}