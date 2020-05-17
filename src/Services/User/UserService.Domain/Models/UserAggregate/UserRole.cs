using System;
using System.Collections.Generic;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.UserAggregate
{
    public class UserRole : ValueObject
    {
        public string RoleName { get; }

        private UserRole(string roleName)
        {
            RoleName = roleName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return RoleName;
        }
        
        public static UserRole Client => new UserRole("Client");
        public static UserRole Manager => new UserRole("Manager");
        public static UserRole Administrator => new UserRole("Administrator");
    }
}