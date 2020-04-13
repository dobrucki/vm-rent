using System;

namespace UserInterface.RestApi.Rentals.CreateRental
{
    public class CreateRentalRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}