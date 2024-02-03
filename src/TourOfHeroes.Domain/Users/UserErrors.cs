using ErrorOr;

namespace TourOfHeroes.Domain.Users
{
    public static class UserErrors
    {
        public static Error AlreadyExists => Error.Validation(
            code: "User.AlreadyExists",
            description: $"User already exists.");

        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found");

        public static Error InvalidLogin => Error.Validation(
            code: "User.InvalidLogin",
            description: $"Invalid login.");
    }
}
