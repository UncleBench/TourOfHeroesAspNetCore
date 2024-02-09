using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TourOfHeroes.Domain.Common.Models;

namespace TourOfHeroes.Infrastructure.Interceptors
{
    public class PublicDomainEventsInterceptor(IPublisher _mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken)
        {
            await PublishDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task PublishDomainEvents(DbContext? dbContext)
        {
            if (dbContext == null) 
            { 
                return; 
            }

            // Get hold of all the various entities
            var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                .Where(entry => entry.Entity.DomainEvents.Any())
                .Select(entry => entry.Entity)
                .ToList();

            // Get hold of all the various domain events
            var domainEvents = entitiesWithDomainEvents.SelectMany(entry => entry.DomainEvents).ToList();

            // Clear domain events from entities
            entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

            // Publish domain events
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
