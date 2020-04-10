using System;
using FluentValidation;

namespace Core.Application.Rentals.CreateRental
{
    public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
    {
        public CreateRentalCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.VirtualMachineId).NotEmpty();
            RuleFor(x => x.StartTime).GreaterThan(DateTime.UtcNow);
            RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        }
    }
}