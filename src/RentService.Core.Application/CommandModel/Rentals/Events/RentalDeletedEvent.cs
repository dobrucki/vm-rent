using System;

namespace RentService.Core.Application.CommandModel.Rentals.Events
{
    public class RentalDeletedEvent : IEvent
    {
        public Guid RentalId { get; set; }
    }
}