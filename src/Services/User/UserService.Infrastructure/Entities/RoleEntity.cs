using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Infrastructure.Entities
{
    public class RoleEntity
    {
        public int RoleEntityId { get; set; }
        public string RoleName { get; set; }
        public List<UserEntityRoleEntity> UserRoles { get; set; }
    }
}    