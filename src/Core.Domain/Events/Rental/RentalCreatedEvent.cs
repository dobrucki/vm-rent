using System;

namespace Application.Domain.Events.Rental
{
    public class RentalCreatedEvent : EventBase
    {
        public Guid RentalId { get; set; }
        
    }
}