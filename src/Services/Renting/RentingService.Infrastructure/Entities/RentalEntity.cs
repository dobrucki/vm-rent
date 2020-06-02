using System;
using RentingService.Domain.Models.CustomerAggregate;
using RentingService.Domain.Models.RentalAggregate;

namespace RentingService.Infrastructure.Entities
{
    public class RentalEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime RentalTime { get; set; }
        public DateTime ReturnTime { get; set; }
    }
}    