using System;

namespace Application.Domain.Models
{
    public class Rental : ModelBase
    {
        public Customer Customer { get; set; }
        public VirtualMachine VirtualMachine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}