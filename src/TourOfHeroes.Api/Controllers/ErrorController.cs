using Microsoft.AspNetCore.Mvc;

namespace TourOfHeroes.Api.Controllers
{
    public sealed class ErrorsController : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
