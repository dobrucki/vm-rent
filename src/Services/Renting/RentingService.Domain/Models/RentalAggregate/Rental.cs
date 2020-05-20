using System;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.RentalAggregate
{
    public class Rental : Entity, IAggregateRoot
    {
        public Customer Customer { get; }
        public Guid VirtualMachineId { get; }
        public RentalStatus RentalStatus { get; }
        
        public Rental(Guid id, Guid customerId, string firstName, string lastName, string emailAddress, Guid virtualMachineId) : base(id)
        {
            VirtualMachineId = virtualMachineId;
            var customer = new Customer(customerId, firstName, lastName, emailAddress);
            Customer = customer;
            RentalStatus = RentalStatus.Started;
        }
    }
}    