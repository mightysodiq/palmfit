using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Palmfit.Core.Dtos;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
using Palmfit.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Palmfit.Core.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;
        private readonly PalmfitDbContext _palmfitDb;
        private readonly UserManager<AppUser> _userManager;

        public AuthRepository(UserManager<AppUser> userManager, IConfiguration configuration, PalmfitDbContext palmfitDb)
        {
            _configuration = configuration;
            _palmfitDb = palmfitDb;
            _userManager = userManager;
        }

        public string GenerateJwtToken(AppUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Set a default expiration in minutes if "AccessTokenExpiration" is missing or not a valid numeric value.
            if (!double.TryParse(jwtSettings["AccessTokenExpiration"], out double accessTokenExpirationMinutes))
            {
                accessTokenExpirationMinutes = 30; // Default expiration of 30 minutes.
            }

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserOTP?> FindMatchingValidOTP(string otpFromUser)
        {
            await RemoveAllExpiredOTP(); // Call the RemoveExpiredOTP method before validation

            var now = DateTime.UtcNow;
            return await _palmfitDb.UserOTPs.FirstOrDefaultAsync(otp => otp.OTP == otpFromUser && otp.Expiration > now);
        }

        public async Task<ApiResponse<string>> UpdateVerifiedStatus(string email)
        {
            var verifiedUser = await _userManager.FindByEmailAsync(email);

            if (verifiedUser == null)
            {
                return new ApiResponse<string>($"{email} Not Found!");
            }
            verifiedUser.IsVerified = true;

            // Save changes to the database
            var identityResult = await _userManager.UpdateAsync(verifiedUser);
            if (!identityResult.Succeeded)
            {
                return new ApiResponse<string>("Verification Failed.");
            }
            return new ApiResponse<string>("Verified successfully.");
        }

        public async Task RemoveAllExpiredOTP()
        {
            var now = DateTime.UtcNow;
            var expiredOTPs = await _palmfitDb.UserOTPs.Where(otp => otp.Expiration <= now).ToListAsync();
            _palmfitDb.UserOTPs.RemoveRange(expiredOTPs);
            await _palmfitDb.SaveChangesAsync();
        }

    }
}
