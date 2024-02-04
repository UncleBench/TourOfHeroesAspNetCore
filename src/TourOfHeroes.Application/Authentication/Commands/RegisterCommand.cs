using ErrorOr;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TourOfHeroes.Application.Authentication.Common;
using TourOfHeroes.Application.Users.Persistence;
using TourOfHeroes.Domain.Users;

namespace TourOfHeroes.Application.Authentication.Commands
{
    public sealed record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password)
        : IRequest<ErrorOr<AuthenticationResult>>;

    public sealed class RegisterCommandHandler(
        IUserRepository _userRepository,
        IJwtTokenGenerator _jwtTokenGenerator)
        : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUser(command.Email, cancellationToken);
            if (existingUser is not null)
            {
                return UserErrors.AlreadyExists;
            }

            User user = User.Create(
                command.FirstName,
                command.LastName,
                command.Email,
                command.Password);

            await _userRepository.CreateUser(user, cancellationToken);
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user,token);
        }
    }
}
