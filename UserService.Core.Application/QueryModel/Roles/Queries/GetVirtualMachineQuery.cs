using System;

namespace UserService.Core.Application.QueryModel.Roles.Queries
{
    public sealed class GetVirtualMachineQuery : IQuery<RoleQueryEntity>
    {
        public Guid VirtualMachineId { get; set; }
    }
}