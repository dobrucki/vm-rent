using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Events
{
    public class RentalFinishedDomainEvent : IDomainEvent
    {
        public Rental Rental { get; set; }
    }
}