using System;

namespace Core.Domain.Dtos
{
    public class RentalDto : ModelBaseDto
    {
        public Guid CustomerId { get; set; }    
        public Guid VirtualMachineId { get; set; }    
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}