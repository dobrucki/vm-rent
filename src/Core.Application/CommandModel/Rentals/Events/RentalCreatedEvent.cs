using Core.Domain.Rentals;

namespace Core.Application.CommandModel.Rentals.Events
{
    public class RentalCreatedEvent : IEvent
    {
        public Rental Rental { get; set; }
    }
}