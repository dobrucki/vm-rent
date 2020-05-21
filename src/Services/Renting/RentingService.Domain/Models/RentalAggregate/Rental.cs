using System;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.RentalAggregate
{
    public class Rental : Entity, IAggregateRoot
    {
        public Customer Customer { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime RentalTime { get; set; }
        public DateTime? ReturnTime { get; set; }

        protected Rental(Guid id) : base(id)
        {
            
        }
        
        public Rental(Guid id, Customer customer, Guid virtualMachineId) : base(id)
        {
            VirtualMachineId = virtualMachineId;
            Customer = customer;
            RentalTime = DateTime.UtcNow;
            ReturnTime = null;
        }    
    }    
}    