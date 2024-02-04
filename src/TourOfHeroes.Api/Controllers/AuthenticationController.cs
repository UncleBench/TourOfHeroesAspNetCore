using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.Application.Authentication.Commands;
using TourOfHeroes.Application.Authentication.Queries;
using TourOfHeroes.Contracts.Authentication;

namespace TourOfHeroes.Api.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public sealed class AuthenticationController(IMediator _mediator, IMapper _mapper) : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var registerCommand = _mapper.Map<RegisterCommand>(request);
            var registerCommandResult = await _mediator.Send(registerCommand, cancellationToken);

            return registerCommandResult.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)), Problem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var loginQuery = _mapper.Map<LoginQuery>(request);
            var loginQueryResult = await _mediator.Send(loginQuery, cancellationToken);

            return loginQueryResult.Match(
                result => Ok(_mapper.Map<AuthenticationResponse>(result)), Problem);
        }
    }
}
