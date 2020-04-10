using FluentValidation;

namespace Core.Application.Rentals.GetRental
{
    public class GetRentalQueryValidator : AbstractValidator<GetRentalQuery>
    {
        public GetRentalQueryValidator()
        {
            RuleFor(x => x.RentalId).NotEmpty();
        }
    }
}