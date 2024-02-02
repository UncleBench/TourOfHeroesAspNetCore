using ErrorOr;
using TourOfHeroes.Contracts.Heroes;

namespace TourOfHeroes.Domain.Heroes
{
    public class Hero
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Hero(Guid id, string name) 
        { 
            Id = id;
            Name = name; 
        }

        public static ErrorOr<Hero> Create(string name, Guid? id = null)
        {
            List<Error> errors = new();

            if (name.Length is < MinNameLength or > MaxNameLength)
            {
                errors.Add(HeroErrors.InvalidName);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new Hero(id ?? Guid.NewGuid(), name);
        }

        public static ErrorOr<Hero> From(CreateHeroRequest request)
        {
            return Create(request.Name);
        }

        public static ErrorOr<Hero> From(Guid id, UpdateHeroRequest request)
        {
            return Create(request.Name, id);
        }
    }
}
