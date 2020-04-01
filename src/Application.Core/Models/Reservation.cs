using System;

namespace Application.Core.Models
{
    public class Reservation
        : ModelBase
    {
        public User User { get; set; }
        public VirtualMachine VirtualMachine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}