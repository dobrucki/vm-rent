using System;

namespace Application.Domain.Events.Customer
{
    public class CustomerCreatedEvent : EventBase
    {
        public Guid CustomerId { get; set; }

    }
}