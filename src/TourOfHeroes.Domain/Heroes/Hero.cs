using TourOfHeroes.Domain.Common;

namespace TourOfHeroes.Domain.Heroes
{
    public sealed class Hero : Entity
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public string Name { get; set; } = null!;

        private Hero()
        {
        }

        private Hero(string name) : base()
        {
            Name = name;
        }

        public static Hero Create(string name)
        {
            return new(name);
        }
    }
}
