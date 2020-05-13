using System;
using Core.Domain.Customers;
using Core.Domain.SharedKernel;
using Core.Domain.VirtualMachines;

namespace Core.Domain.Rentals
{
    public class Rental : ModelBase
    {
        public virtual Customer Customer { get; set; }
        public virtual VirtualMachine VirtualMachine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}