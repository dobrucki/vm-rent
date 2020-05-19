using System;
using System.Collections.Generic;

namespace UserService.Infrastructure.Entities
{
    public class UserEntity
    {
        public int UserEntityId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public List<UserEntityRoleEntity> UserRoles { get; set; }
    }
}    