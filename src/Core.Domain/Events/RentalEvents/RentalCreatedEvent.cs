using System;

namespace Application.Domain.Events.RentalEvents
{
    public class RentalCreatedEvent : EventBase
    {
        public Guid RentalId { get; set; }
        
    }
}