using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Application.QueryModel.Roles.Queries;
using UserService.Core.Application.SharedKernel.Exceptions;

namespace UserService.Core.Application.QueryModel.Roles
{
    internal sealed class RoleQueryHandler :
        IQueryHandler<GetVirtualMachineQuery, RoleQueryEntity>,
        IQueryHandler<ListVirtualMachinesQuery, IList<RoleQueryEntity>>
    {
        private readonly IRolesQueryRepository _roles;

        public RoleQueryHandler(IRolesQueryRepository roles)
        {
            _roles = roles;
        }

        public async Task<RoleQueryEntity> Handle(
            GetVirtualMachineQuery request, CancellationToken cancellationToken)
        {
            var virtualMachine = await _roles.GetRoleByIdAsync(request.VirtualMachineId);
            return virtualMachine ?? throw new NotFoundException("Role", request.VirtualMachineId);
        }

        public async Task<IList<RoleQueryEntity>> Handle(
            ListVirtualMachinesQuery request, CancellationToken cancellationToken)
        {
            return await _roles.ListRolesAsync(request.Limit, request.Offset) 
                   ?? new List<RoleQueryEntity>(0);
        }
    }
}