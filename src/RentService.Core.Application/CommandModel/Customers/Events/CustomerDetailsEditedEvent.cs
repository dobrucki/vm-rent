using Core.Domain.Customers;

namespace RentService.Core.Application.CommandModel.Customers.Events
{
    public class CustomerDetailsEditedEvent : IEvent
    {
        public Customer Customer { get; set; }
    }
}