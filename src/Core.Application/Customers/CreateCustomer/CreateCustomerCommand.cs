using MediatR;

namespace Core.Application.Customers.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CustomerDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}