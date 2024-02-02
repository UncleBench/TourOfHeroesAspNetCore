using ErrorOr;
using TourOfHeroes.Domain.Common;

namespace TourOfHeroes.Domain.Heroes
{
    public class Hero : Entity
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public string Name { get; } = null!;

        public Hero(string name, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            Name = name;
        }

        private Hero()
        {
        }
    }
}
