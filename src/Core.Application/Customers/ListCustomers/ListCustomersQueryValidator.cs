using Core.Application.Rentals.ListRentals;
using FluentValidation;

namespace Core.Application.Customers.ListCustomers
{
    public class ListCustomersQueryValidator : AbstractValidator<ListRentalsQuery>
    {
        public ListCustomersQueryValidator()
        {
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        }
    }
}