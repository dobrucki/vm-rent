using FluentValidation;

namespace Core.Application.VirtualMachines.DeleteVirtualMachine
{
    public class DeleteVirtualMachineCommandValidator : AbstractValidator<DeleteVirtualMachineCommand>
    {
        public DeleteVirtualMachineCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}