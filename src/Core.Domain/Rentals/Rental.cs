using System;
using Core.Domain.Customers;
using Core.Domain.SharedKernel;
using Core.Domain.VirtualMachines;

namespace Core.Domain.Rentals
{
    public class Rental : ModelBase
    {
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}