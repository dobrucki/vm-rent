using FluentValidation;

namespace Core.Application.Rentals.Queries.ListRentals
{
    public class ListRentalsQueryValidator : AbstractValidator<ListRentalsQuery>
    {
        public ListRentalsQueryValidator()
        {
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        }
    }
}