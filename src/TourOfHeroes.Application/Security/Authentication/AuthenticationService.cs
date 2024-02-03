using System;
using TourOfHeroes.Application.Common.Authentication;

namespace TourOfHeroes.Application.Security.Authentication
{
    internal class AuthenticationService(IJwtTokenGenerator _jwtTokenGenerator) : IAuthenticationService
    {
        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(),
                "john",
                "doe",
                "john.doe@test.com",
                "token");
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // TODO: Check if user exists

            // TODO: Create user

            // TODO: Create token
            Guid userId = Guid.NewGuid();

            var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

            return new AuthenticationResult(
                userId,
                firstName,
                lastName,
                email,
                token);
        }
    }
}
