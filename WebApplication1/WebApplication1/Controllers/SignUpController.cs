using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IAppUserRepository _userRepository;

        public SignUpController(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] SignUpDto userRequest)
        {
            var res = await _userRepository.CreateUser(userRequest);
            if (res != null)
            {
                return Ok(ApiResponse.Success(res));
            }
            return BadRequest(ApiResponse.Failed(res));

        }
    }
}
