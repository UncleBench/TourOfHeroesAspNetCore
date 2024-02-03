using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Security.Authentication
{
    public sealed record AuthenticationResult(
        User User,
        string Token);
}
