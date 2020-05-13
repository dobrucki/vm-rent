using Core.Domain.Rentals;

namespace RentService.Core.Application.CommandModel.Rentals.Events
{
    public class RentalCreatedEvent : IEvent
    {
        public Rental Rental { get; set; }
    }
}