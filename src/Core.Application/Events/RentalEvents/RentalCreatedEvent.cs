using System;

namespace Core.Application.Events.RentalEvents
{
    public class RentalCreatedEvent : EventBase
    {
        public Guid RentalId { get; set; }
        
    }
}