using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Authentication.Common;
using TourOfHeroes.Application.Users.Persistence;
using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Authentication.Queries
{
    public sealed record LoginQuery(
        string Email,
        string Password) 
        : IRequest<ErrorOr<AuthenticationResult>>;

    public sealed class LoginCommandHandler(
        IUserRepository _userRepository,
        IJwtTokenGenerator _jwtTokenGenerator)
        : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(query.Email, cancellationToken);

            if (user is null)
            {
                return UserErrors.InvalidCredentials;
            }

            if (user.Password != query.Password)
            {
                return UserErrors.InvalidCredentials;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
