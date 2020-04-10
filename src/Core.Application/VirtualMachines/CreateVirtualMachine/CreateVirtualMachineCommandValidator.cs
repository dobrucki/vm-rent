using FluentValidation;

namespace Core.Application.VirtualMachines.CreateVirtualMachine
{
    public class CreateVirtualMachineCommandValidator : AbstractValidator<CreateVirtualMachineCommand>
    {
        public CreateVirtualMachineCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}