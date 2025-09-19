using CarRentalSystem.Application.DTOs.Auth;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRentalSystem.API.Controllers.Auth
{
    // api/Auth
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        // api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
            => Ok(await _auth.RegisterAsync(dto));

        // api/Auth/register
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
            => Ok(await _auth.LoginAsync(dto));

        // api/Auth/verify-email
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            await _auth.VerifyEmailAsync(new VerifyEmailDto { Token = token });
            return Ok("Email verified successfully.");
        }

        // api/Auth/send-verification
        [Authorize]
        [HttpPost("send-verification")]
        public async Task<IActionResult> SendVerification()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            await _auth.SendVerificationEmailAsync(userId, baseUrl);
            return Ok("Verification email sent.");
        }

        // api/Auth/google-login
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
            => Ok(await _auth.GoogleLoginAsync(dto));


        // Staff flow
        [Authorize(Roles = "Admin")]
        [HttpPost("invite-staff")]
        public async Task<IActionResult> InviteStaff([FromBody] InviteStaffDto dto)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
            await _auth.InviteStaffAsync(dto.Email, baseUrl);
            return Ok("Staff invite sent.");
        }

        [HttpPost("register-staff")]
        public async Task<IActionResult> RegisterStaff(RegisterStaffDto dto)
        {
            await _auth.RegisterStaffAsync(dto);
            return Ok("Staff registered successfully.");
        }
    }
}
