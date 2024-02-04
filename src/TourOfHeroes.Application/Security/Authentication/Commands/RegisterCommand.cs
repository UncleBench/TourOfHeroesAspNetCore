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
            var response = new AuthenticationResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token
            );

            return response;
        }
    }
}
