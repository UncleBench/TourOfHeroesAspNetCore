using ErrorOr;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TourOfHeroes.Contracts.Authentication;
using System;

namespace TourOfHeroes.Application.Security.Authentication.Commands
{
    public record LoginCommand(
        string Email,
        string Password)
        : IRequest<ErrorOr<AuthenticationResponse>>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<AuthenticationResponse>>
    {
        public Task<ErrorOr<AuthenticationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return new Task<ErrorOr<AuthenticationResponse>>(() => new AuthenticationResponse(
                Guid.NewGuid(),
                "john",
                "doe",
                "john.doe@test.com",
                "token")
            );
        }
    }
}
