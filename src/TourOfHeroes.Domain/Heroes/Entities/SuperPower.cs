using TourOfHeroes.Domain.Common.Models;
using TourOfHeroes.Domain.Heroes.ValueObjects;

namespace TourOfHeroes.Domain.Heroes.Entities
{
    public sealed class SuperPower : Entity<SuperPowerId>
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public const int MinDescriptionLength = 3;
        public const int MaxDescriptionLength = 50;

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        private SuperPower(
            SuperPowerId superPowerId, 
            string name, 
            string description) 
            : base(superPowerId)
        {
            Name = name;
            Description = description;
        }

        public static SuperPower Create(string name, string description)
        {
            return new(SuperPowerId.CreateUnique(), name, description);
        }

        private SuperPower()
        {
        }
    }
}
