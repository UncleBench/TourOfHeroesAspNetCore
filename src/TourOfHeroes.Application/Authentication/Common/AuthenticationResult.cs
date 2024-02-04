using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}
