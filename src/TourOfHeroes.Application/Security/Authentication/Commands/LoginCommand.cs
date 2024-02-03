using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Contracts.Authentication;

namespace TourOfHeroes.Application.Security.Authentication.Commands
{
    public sealed record LoginCommand(string Email, string Password) : IRequest<ErrorOr<AuthenticationResponse>>;

    public sealed class LoginCommandHandler(IAuthenticationService _authenticationService) : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResponse>>
    {
        public async Task<ErrorOr<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var authResult = await _authenticationService.Login(request.Email, request.Password, cancellationToken);

            if (authResult.IsError)
            {
                return authResult.Errors;
            }

            return new AuthenticationResponse(
                authResult.Value.User.Id,
                authResult.Value.User.FirstName,
                authResult.Value.User.LastName,
                authResult.Value.User.Email,
                authResult.Value.Token
            );
        }
    }
}
