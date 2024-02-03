using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Common.Authentication;
using TourOfHeroes.Application.Users.Persistence;
using TourOfHeroes.Contracts.Authentication;
using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Security.Authentication.Commands
{
    public sealed record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password)
        : IRequest<ErrorOr<AuthenticationResponse>>;

    public sealed class RegisterCommandHandler(
        IUserRepository _userRepository,
        IJwtTokenGenerator _jwtTokenGenerator) 
        : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResponse>>
    {
        public async Task<ErrorOr<AuthenticationResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUser(command.Email, cancellationToken) is not null)
            {
                return UserErrors.AlreadyExists;
            }

            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            await _userRepository.CreateUser(user, cancellationToken);
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
