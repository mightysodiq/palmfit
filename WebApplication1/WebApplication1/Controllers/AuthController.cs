using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Palmfit.Core.Dtos;
using Palmfit.Core.Implementations;
using Palmfit.Core.Services;
using Palmfit.Data.Entities;

namespace Palmfit.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailServices;


        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, IAuthRepository authRepo, IEmailServices emailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authRepo = authRepo;
            _emailServices = emailServices;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>("Invalid request. Please provide a valid email and password."));
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user == null)
                {
                    return NotFound(new ApiResponse<string>("User not found. Please check your email and try again."));
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return Unauthorized(new ApiResponse<string>("Invalid credentials. Please check your email or password and try again."));
                }
                else
                {
                    var token = _authRepo.GenerateJwtToken(user);
                    Response.Headers.Add("Authorization", "Bearer " + token);
                    return Ok(new ApiResponse<string>("Login successful."));

                }
            }
        }
        [HttpPost("password-reset")]
        public async Task<ActionResult<ApiResponse>> SendPasswordResetEmail(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    return ApiResponse.Failed("Invalid email address.");
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetUrl = "https://your-app.com/reset-password?token=" + token;
                var emailBody = $"Click the link below to reset your password: {passwordResetUrl}";
                await _emailServices.SendEmailAsync(loginDto.Email, "Password Reset", emailBody);
                return ApiResponse.Success("Password reset email sent successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse.Failed(null, "An error occurred during password reset.", new List<string> { ex.Message });
            }
        }


        [HttpPost("Validate-OTP")]
        public async Task<IActionResult> ValidateOTP([FromBody] OtpDto otpFromUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>("Invalid OTP, check and try again"));
            }
            var userOTP = await _authRepo.FindMatchingValidOTP(otpFromUser.Otp);
            if (userOTP == null)
            {
                return BadRequest(new ApiResponse<string>("Invalid OTP, check and try again"));
            }

            await _authRepo.UpdateVerifiedStatus(otpFromUser.Email);

            return Ok(new ApiResponse<string>("Validation Successfully."));
        }
    }
}
