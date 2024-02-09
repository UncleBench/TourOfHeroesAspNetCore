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

        public static class SuperPowerErrors
        {
            public static Error InvalidName => Error.Validation(
                        code: "Hero.SuperPower.InvalidName",
                        description: $"Super power name must be at least {Entities.SuperPower.MinNameLength}" +
                            $" characters long and at most {Entities.SuperPower.MaxNameLength} characters long.");

            public static Error InvalidDescription => Error.Validation(
                        code: "Hero.SuperPower.InvalidDescription",
                        description: $"Super power description must be at least {Entities.SuperPower.MinDescriptionLength}" +
                            $" characters long and at most {Entities.SuperPower.MaxDescriptionLength} characters long.");

            public static Error NotFound => Error.NotFound(
            code: "Hero.SuperPower.NotFound",
            description: "Super power not found");
        }
    }
}
