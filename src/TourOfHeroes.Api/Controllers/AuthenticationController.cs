using MediatR;
using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.Application.Security.Authentication.Commands;
using TourOfHeroes.Application.Security.Authentication.Queries;
using TourOfHeroes.Contracts.Authentication;

namespace TourOfHeroes.Api.Controllers
{
    [Route("auth")]
    public sealed class AuthenticationController(IMediator _mediator) : ApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var loginCommand = new LoginQuery(request.Email, request.Password);
            var loginCommandResult = await _mediator.Send(loginCommand);

            return loginCommandResult.Match(Ok, Problem);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var registerCommand = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            var registerCommandResult = await _mediator.Send(registerCommand);

            return registerCommandResult.Match(Ok, Problem);
        }
    }
}
