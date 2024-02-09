using TourOfHeroes.Domain.Common.Models;

namespace TourOfHeroes.Domain.Heroes.Events
{
    public record HeroCreated(Hero Hero) : IDomainEvent;
}
