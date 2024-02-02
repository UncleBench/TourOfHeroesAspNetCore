using ErrorOr;

namespace TourOfHeroes.Domain.Heroes
{
    public static class HeroErrors
    {
        public static Error InvalidName => Error.Validation(
            code: "Hero.InvalidName",
            description: $"Hero name must be at least {Hero.MinNameLength}" +
                $" characters long and at most {Hero.MaxNameLength} characters long.");

        public static Error NotFound => Error.NotFound(
            code: "Hero.NotFound",
            description: "Hero not found");
    }
}
