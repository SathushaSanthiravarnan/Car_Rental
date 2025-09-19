using Asp.Versioning;
using CarRentalSystem.Application.Commands.Auth.ForgotPassword;
using CarRentalSystem.Application.Commands.Auth.GoogleSignIn;
using CarRentalSystem.Application.Commands.Auth.Login;
using CarRentalSystem.Application.Commands.Auth.Logout;
using CarRentalSystem.Application.Commands.Auth.RefreshToken;
using CarRentalSystem.Application.Commands.Auth.RegisterGuest;
using CarRentalSystem.Application.Commands.Auth.ResendVerification;
using CarRentalSystem.Application.Commands.Auth.ResetPassword;
using CarRentalSystem.Application.DTOs.Auth;
using CarRentalSystem.Application.Queries.Auth.ConfirmEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.Auth
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator) => _mediator = mediator;

        // POST: /api/v1/auth/register-guest
        [HttpPost("register-guest")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterGuest([FromBody] RegisterGuestDto dto, CancellationToken ct)
        {
            var userId = await _mediator.Send(new RegisterGuestCommand(dto), ct);
            return Ok(new { userId });
        }

        // POST: /api/v1/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct)
        {
            // pass IP & UserAgent to handler for refresh-token metadata
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            var ua = Request.Headers.UserAgent.ToString();

            var result = await _mediator.Send(new LoginCommand(dto, ip, ua), ct);
            return Ok(result);
        }

        // POST: /api/v1/auth/refresh-token
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto, CancellationToken ct)
        {
            var result = await _mediator.Send(new RefreshTokenCommand(dto), ct);
            return Ok(result);
        }

        // POST: /api/v1/auth/google-signin
        [HttpPost("google-signin")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInDto dto, CancellationToken ct)
        {
            var result = await _mediator.Send(new GoogleSignInCommand(dto), ct);
            return Ok(result);
        }

        // POST: /api/v1/auth/resend-verification
        [HttpPost("resend-verification")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationDto dto, CancellationToken ct)
        {
            var ok = await _mediator.Send(new ResendVerificationCommand(dto), ct);
            return ok ? NoContent() : BadRequest();
        }

        // GET: /api/v1/auth/confirm-email?email=...&token=...
        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token, CancellationToken ct)
        {
            var ok = await _mediator.Send(new ConfirmEmailQuery(email, token), ct);
            return ok ? NoContent() : BadRequest();
        }

        // POST: /api/v1/auth/forgot-password
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto, CancellationToken ct)
        {
            // Always return 204 to avoid email enumeration
            _ = await _mediator.Send(new ForgotPasswordCommand(dto), ct);
            return NoContent();
        }

        // POST: /api/v1/auth/reset-password
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto, CancellationToken ct)
        {
            var ok = await _mediator.Send(new ResetPasswordCommand(dto), ct);
            return ok ? NoContent() : BadRequest();
        }

        // POST: /api/v1/auth/logout
        [HttpPost("logout")]
        [AllowAnonymous] // can be anonymous; we revoke by refresh token
        public async Task<IActionResult> Logout([FromBody] LogoutDto dto, CancellationToken ct)
        {
            var ok = await _mediator.Send(new LogoutCommand(dto), ct);
            return ok ? NoContent() : BadRequest();
        }
    }
}