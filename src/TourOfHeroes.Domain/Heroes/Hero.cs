using System.ComponentModel.Design;
using TourOfHeroes.Domain.Common.Models;
using TourOfHeroes.Domain.Heroes.Entities;
using TourOfHeroes.Domain.Heroes.Events;
using TourOfHeroes.Domain.Heroes.ValueObjects;

namespace TourOfHeroes.Domain.Heroes
{
    public sealed class Hero : AggregateRoot<HeroId, Guid>
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public List<SuperPower> SuperPowers = [];

        public string Name { get; set; } = null!;

        private Hero(
            HeroId heroId,
            string name,
            List<SuperPower> superPowers)
            : base(heroId)
        {
            Name = name;
            SuperPowers = superPowers;
        }

        public static Hero Create(string name, List<SuperPower>? superPowers)
        {
            var hero = new Hero(HeroId.CreateUnique(), name, superPowers ?? []);

            hero.AddDomainEvent(new HeroCreated(hero));

            return hero;
        }

        private Hero()
        {
        }
    }
}
