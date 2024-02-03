using System;

namespace TourOfHeroes.Application.Security.Authentication
{
    public record AuthenticationResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
