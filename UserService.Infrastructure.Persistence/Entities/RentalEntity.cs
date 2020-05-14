using System;

namespace RentService.Infrastructure.Persistence.Entities
{
    public class RentalEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public Guid? VirtualMachineId { get; set; }
        public virtual VirtualMachineEntity VirtualMachine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}