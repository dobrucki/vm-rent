using System;
using System.Collections.Generic;
using UserService.Domain.Events;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public string Password { get; private set; }

        private List<UserRole> _roles;
        public IEnumerable<UserRole> Roles => _roles.AsReadOnly();
        
        
        public User(Guid id, string firstName, string lastName, string emailAddress, string password) : base(id)
        {
            _roles = new List<UserRole>();
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Password = password;
            
            var @event = new UserCreatedDomainEvent(this);
            AddDomainEvent(@event);
        }    

        public void AddRole(UserRole userRole)
        {
            _roles.Add(userRole);
            
            var @event = new UserRoleAddedDomainEvent(this, userRole);
            AddDomainEvent(@event);
        }

        public void RemoveRole(UserRole userRole)
        {
            _roles.Remove(userRole);
            
            var @event = new UserRoleRemovedDomainEvent(this, userRole);
            AddDomainEvent(@event);
        }

        public void ChangeUserPassword(string password)
        {
            Password = password;
            
            var @event = new UserPasswordUpdated(this, password);
            AddDomainEvent(@event);
        }

    }
}    