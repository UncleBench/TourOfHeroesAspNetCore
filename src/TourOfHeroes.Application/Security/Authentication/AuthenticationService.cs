using ErrorOr;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Common.Authentication;
using TourOfHeroes.Application.Users.Persistence;
using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Security.Authentication
{
    internal sealed class AuthenticationService(IJwtTokenGenerator _jwtTokenGenerator, IUserRepository _userRepository) : IAuthenticationService
    {
        public async Task<ErrorOr<AuthenticationResult>> Login(
            string email,
            string password,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(email, cancellationToken);

            if (user is null)
            {
                return UserErrors.InvalidCredentials;
            }

            if (user.Password != password)
            {
                return UserErrors.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

        public async Task<ErrorOr<AuthenticationResult>> Register(
            string firstName,
            string lastName,
            string email,
            string password,
            CancellationToken cancellationToken)
        {
            if (_userRepository.GetUser(email, cancellationToken) is not null)
            {
                return UserErrors.AlreadyExists;
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            await _userRepository.CreateUser(user, cancellationToken);
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
