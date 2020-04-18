using FluentValidation;

namespace Core.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}