using System;

namespace Core.Domain.Events.RentalEvents
{
    public class RentalCreatedEvent : EventBase
    {
        public Guid RentalId { get; set; }
        
    }
}