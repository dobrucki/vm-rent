using FluentValidation;

namespace Core.Application.Customers.Commands.EditCustomerDetails
{
    public class EditCustomerDetailsCommandValidator : AbstractValidator<EditCustomerDetailsCommand>
    {
        public EditCustomerDetailsCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}