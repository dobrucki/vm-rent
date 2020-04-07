using FluentValidation;

namespace Core.Application.Customers.GetCustomer
{
    public class GetCustomerQueryValidator1 : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator1()    
        {
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}