using System;
using System.Collections.Generic;
using UserService.Domain.Events;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string Login { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public string Password { get; private set; }
        
        public bool IsActive { get; private set; }

        private List<Role> _roles;
        public IEnumerable<Role> Roles => _roles.AsReadOnly();
        
        
        public User(Guid id, string login, string firstName, string lastName, string emailAddress, string password) 
            : base(id)
        {
            _roles = new List<Role> { Role.Client };
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;    
            Password = password;
            Login = login;
            IsActive = false;    
                    
            var @event = new UserCreatedDomainEvent(id, firstName, lastName, emailAddress);
            AddDomainEvent(@event);
        }    

        public void AddRole(Role role)
        {
            _roles.Add(role);
            
            var @event = new UserRoleAddedDomainEvent(this, role);
            AddDomainEvent(@event);
        }

        public void RemoveRole(Role role)
        {
            _roles.Remove(role);
            
            var @event = new UserRoleRemovedDomainEvent(this, role);
            AddDomainEvent(@event);
        }

        public void ChangeUserPassword(string password)
        {
            Password = password;
            
            var @event = new UserPasswordUpdated(this, password);
            AddDomainEvent(@event);
        }

        public void ActivateUser()
        {
            IsActive = true;
        }

        public void DeactivateUser()
        {
            IsActive = false;
        }

    }
}    