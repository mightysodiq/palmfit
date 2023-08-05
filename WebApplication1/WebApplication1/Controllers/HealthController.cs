using Microsoft.AspNetCore.Mvc;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("ping")]
        public Task<IActionResult> Ping()
        {
            return Task.FromResult<IActionResult>(Ok("pong"));
        }
    }
}