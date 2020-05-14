using System.Collections.Generic;

namespace UserService.Core.Application.QueryModel.Roles.Queries
{
    public sealed class ListVirtualMachinesQuery : IQuery<IList<RoleQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}