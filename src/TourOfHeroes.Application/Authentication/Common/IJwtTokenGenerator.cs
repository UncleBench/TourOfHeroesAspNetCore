using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Authentication.Common
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
