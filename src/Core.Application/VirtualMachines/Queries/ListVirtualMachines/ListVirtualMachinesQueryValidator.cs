using FluentValidation;

namespace Core.Application.VirtualMachines.Queries.ListVirtualMachines
{
    public class ListVirtualMachinesQueryValidator : AbstractValidator<ListVirtualMachinesQuery>
    {
        public ListVirtualMachinesQueryValidator()
        {
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
            RuleFor(x => x.Offset).GreaterThanOrEqualTo(0);
        }
    }
}