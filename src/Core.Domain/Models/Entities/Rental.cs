using System;

namespace Core.Domain.Models.Entities
{
    public class Rental : ModelBase
    {
        public Customer Customer { get; set; }
        public VirtualMachine VirtualMachine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}