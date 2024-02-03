using ErrorOr;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Contracts.Authentication;

namespace TourOfHeroes.Application.Security.Authentication.Commands
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password)
        : IRequest<ErrorOr<AuthenticationResponse>>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResponse>>
    {
        public Task<ErrorOr<AuthenticationResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return new Task<ErrorOr<AuthenticationResponse>>(() => new AuthenticationResponse(
                Guid.NewGuid(), 
                request.FirstName, 
                request.LastName, 
                request.Email, 
                "token")
            );
        }
    }
}
