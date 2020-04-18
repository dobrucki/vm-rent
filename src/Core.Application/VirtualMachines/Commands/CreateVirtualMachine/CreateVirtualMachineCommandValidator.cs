using FluentValidation;

namespace Core.Application.VirtualMachines.Commands.CreateVirtualMachine
{
    public class CreateVirtualMachineCommandValidator : AbstractValidator<CreateVirtualMachineCommand>
    {
        public CreateVirtualMachineCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}