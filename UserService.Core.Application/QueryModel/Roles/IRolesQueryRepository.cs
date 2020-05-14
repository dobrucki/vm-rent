using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserService.Core.Application.QueryModel.Roles
{
    public interface IRolesQueryRepository
    {
        Task<RoleQueryEntity> GetRoleByIdAsync(Guid roleId);
        Task<IList<RoleQueryEntity>> ListRolesAsync(int limit, int offset);
    }
}