using Core.Domain.Customers;

namespace Core.Application.CommandModel.Customers.Events
{
    public class CustomerDetailsEditedEvent : IEvent
    {
        public Customer Customer { get; set; }
    }
}