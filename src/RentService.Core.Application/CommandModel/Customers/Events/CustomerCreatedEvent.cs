using Core.Domain.Customers;

namespace RentService.Core.Application.CommandModel.Customers.Events
{
    public class CustomerCreatedEvent : IEvent
    {
        public Customer Customer { get; set; }
    }
}