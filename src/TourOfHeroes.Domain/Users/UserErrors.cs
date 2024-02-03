using ErrorOr;

namespace TourOfHeroes.Domain.Users
{
    public static class UserErrors
    {
        public static Error AlreadyExists => Error.Validation(
            code: "User.AlreadyExists",
            description: $"User already exists.");

        public static Error InvalidCredentials => Error.Validation(
            code: "User.InvalidCredentials",
            description: $"Invalid credentials.");
    }
}
