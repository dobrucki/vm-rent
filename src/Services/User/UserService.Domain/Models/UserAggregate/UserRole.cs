using System;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.UserAggregate
{
    public class UserRole : Entity
    {
        public string RoleName { get; private set; }
        
        public Guid RoleId { get; private set; }
        
        public UserRole(Guid id, Guid roleId, string roleName) : base(id)
        {
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}