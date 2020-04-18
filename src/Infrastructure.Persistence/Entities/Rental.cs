using System;

namespace Infrastructure.Persistence.Entities
{
    public class RentalEntity
    {
        public Guid Id { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        public virtual VirtualMachineEntity VirtualMachine { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}