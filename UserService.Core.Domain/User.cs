using System;
using System.Collections.Generic;

namespace UserService.Core.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}