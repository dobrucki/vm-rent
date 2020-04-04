using System;

namespace UserInterface.RestApi.Requests.RentalsRequests
{
    public class CreateRentalRequest
    {
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}