using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.Application.Security.Authentication.Commands;
using TourOfHeroes.Application.Security.Authentication.Queries;
using TourOfHeroes.Contracts.Authentication;

namespace TourOfHeroes.Api.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public sealed class AuthenticationController(IMediator _mediator) : ApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var loginQuery = new LoginQuery(request.Email, request.Password);
            var loginQueryResult = await _mediator.Send(loginQuery, cancellationToken);

            return loginQueryResult.Match(Ok, Problem);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        {
            var registerCommand = new RegisterCommand(
                request.FirstName, 
                request.LastName, 
                request.Email, 
                request.Password);

            var registerCommandResult = await _mediator.Send(registerCommand, cancellationToken);

            return registerCommandResult.Match(Ok, Problem);
        }
    }
}
