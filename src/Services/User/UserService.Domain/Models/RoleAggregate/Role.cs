using System;
using UserService.Domain.Events;
using UserService.Domain.SeedWork;

namespace UserService.Domain.Models.RoleAggregate
{
    public class Role : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        
        public Role(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public void UpdateName(string name)
        {
            Name = name;
            
            var @event = new RoleNameUpdatedDomainEvent(this, name);
            AddDomainEvent(@event);
        }
    }
}    