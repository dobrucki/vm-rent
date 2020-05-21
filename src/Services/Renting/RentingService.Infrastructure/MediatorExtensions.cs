using System.Linq;
using System.Threading.Tasks;
using MediatR;
using RentingService.Domain.SeedWork;

namespace RentingService.Infrastructure
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, RentingServiceContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(entity => (entity.Entity.DomainEvents?.Any()).GetValueOrDefault(false))
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(entity => entity.Entity.DomainEvents)
                .ToList();
            
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}