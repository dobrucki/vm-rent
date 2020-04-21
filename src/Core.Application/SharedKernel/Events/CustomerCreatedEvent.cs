using Core.Application.Customers;
using Core.Domain.Customers;
using MediatR;

namespace Core.Application.SharedKernel.Events
{
    public class CustomerCreatedEvent : INotification
    {
        public Customer Customer { get; set; }
    }
}    