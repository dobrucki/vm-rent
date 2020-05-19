using System;
using System.Collections.Generic;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.UserAggregate
{
    public class Role : ValueObject
    {
        public string RoleName { get; }

        private Role(string roleName)
        {
            RoleName = roleName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return RoleName;
        }
        
        public static Role Client => new Role("Client");
        public static Role Manager => new Role("Manager");
        public static Role Administrator => new Role("Administrator");
    }
}