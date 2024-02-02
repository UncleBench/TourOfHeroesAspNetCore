using ErrorOr;
using TourOfHeroes.Domain.Common;

namespace TourOfHeroes.Domain.Heroes
{
    public class Hero : Entity
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public string Name { get; } = null!;

        public Hero(string name, Guid id) : base(id)
        {
            Name = name;
        }

        private Hero()
        {
        }

        public static ErrorOr<Hero> Create(string name, Guid? id = null)
        {
            List<Error> errors = [];

            if (name.Length is < MinNameLength or > MaxNameLength)
            {
                errors.Add(HeroErrors.InvalidName);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Hero(name, id ?? Guid.NewGuid());
        }
    }
}
