using Microsoft.AspNetCore.Mvc;

namespace TourOfHeroes.Server.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
