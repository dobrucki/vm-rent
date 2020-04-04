using System;

namespace Core.Application.Events.CustomerEvents
{
    public class CustomerCreatedEvent : EventBase
    {
        public Guid CustomerId { get; set; }

    }
}