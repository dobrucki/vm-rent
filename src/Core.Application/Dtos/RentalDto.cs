using System;

namespace Core.Application.Dtos
{
    public class RentalDto : ModelBaseDto
    {
        public Guid CustomerId { get; set; }    
        public Guid VirtualMachineId { get; set; }    
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}