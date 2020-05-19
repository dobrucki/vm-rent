using System;
using System.Collections.Generic;

namespace UserService.Domain.SeedWork
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents;
        public IEnumerable<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private readonly Guid _id;
        public Guid Id => _id;

        private Entity()
        { }
        
        protected Entity(Guid id)
        {
            _domainEvents = new List<IDomainEvent>();
            _id = id;
        }

        public void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}