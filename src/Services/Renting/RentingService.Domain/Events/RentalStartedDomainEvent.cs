using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Events
{
    public class RentalStartedDomainEvent : IDomainEvent
    {
        public Rental Rental { get; set; }
    }
}