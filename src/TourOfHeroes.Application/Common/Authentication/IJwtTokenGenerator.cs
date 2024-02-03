using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Common.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
