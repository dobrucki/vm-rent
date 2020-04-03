using System;

namespace Core.Domain.Events.CustomerEvents
{
    public class CustomerCreatedEvent : EventBase
    {
        public Guid CustomerId { get; set; }

    }
}