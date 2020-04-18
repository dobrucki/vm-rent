using FluentValidation;

namespace Core.Application.Rentals.Queries.GetRental
{
    public class GetRentalQueryValidator : AbstractValidator<GetRentalQuery>
    {
        public GetRentalQueryValidator()
        {
            RuleFor(x => x.RentalId).NotEmpty();
        }
    }
}