using System;

namespace Application.Domain.Events.CustomerEvents
{
    public class CustomerCreatedEvent : EventBase
    {
        public Guid CustomerId { get; set; }

    }
}