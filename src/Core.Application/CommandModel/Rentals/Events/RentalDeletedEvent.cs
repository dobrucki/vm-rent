using System;

namespace Core.Application.CommandModel.Rentals.Events
{
    public class RentalDeletedEvent : IEvent
    {
        public Guid RentalId { get; set; }
    }
}