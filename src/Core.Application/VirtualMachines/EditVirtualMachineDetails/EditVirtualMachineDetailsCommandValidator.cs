using FluentValidation;

namespace Core.Application.VirtualMachines.EditVirtualMachineDetails
{
    public class EditVirtualMachineDetailsCommandValidator : AbstractValidator<EditVirtualMachineDetailsCommand>
    {
        public EditVirtualMachineDetailsCommandValidator()
        {
            RuleFor(x => x.VirtualMachineId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}