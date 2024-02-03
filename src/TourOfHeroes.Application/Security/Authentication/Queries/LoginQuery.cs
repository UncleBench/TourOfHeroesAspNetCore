using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Common.Authentication;
using TourOfHeroes.Application.Users.Persistence;
using TourOfHeroes.Contracts.Authentication;
using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Security.Authentication.Queries
{
    public sealed record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResponse>>;

    public sealed class LoginCommandHandler(
        IUserRepository _userRepository, 
        IJwtTokenGenerator _jwtTokenGenerator) 
        : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResponse>>
    {
        public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginQuery query, CancellationToken cancellationToken)
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

            return new AuthenticationResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token
            );
        }
    }
}
