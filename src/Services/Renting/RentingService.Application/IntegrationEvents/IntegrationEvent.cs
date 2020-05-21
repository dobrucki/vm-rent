using System;

namespace RentingService.Application.IntegrationEvents
{
    public class IntegrationEvent
    {
        public Guid Id { get; set; }

        public IntegrationEvent(Guid id)
        {
            Id = id;
        }
    }
}