using FluentValidation;

namespace Core.Application.VirtualMachines.GetVirtualMachine
{
    public class GetVirtualMachineQueryValidator : AbstractValidator<GetVirtualMachineQuery>
    {
        public GetVirtualMachineQueryValidator()
        {
            RuleFor(x => x.VirtualMachineId).NotEmpty();
        }
    }
}