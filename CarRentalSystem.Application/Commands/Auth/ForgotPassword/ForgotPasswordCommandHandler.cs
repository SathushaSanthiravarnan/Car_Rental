using System.Security.Cryptography;
using CarRentalSystem.Application.Interfaces.Notifications;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CarRentalSystem.Application.Commands.Auth.ForgotPassword
{
    public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IUserRepository _users;
        private readonly IEmailVerificationRepository _verifications;
        private readonly IEmailSender _email;
        private readonly IConfiguration _cfg;

        public ForgotPasswordCommandHandler(
            IUserRepository users,
            IEmailVerificationRepository verifications,
            IEmailSender email,
            IConfiguration cfg)
        {
            _users = users; _verifications = verifications; _email = email; _cfg = cfg;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken ct)
        {
            var user = await _users.FindByEmailAsync(request.Dto.Email, ct);
            if (user is null) return true; // don't reveal

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
            await _verifications.AddAsync(new Domain.Entities.EmailVerification
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(2),
                IsUsed = false
            }, ct);
            await _verifications.SaveChangesAsync(ct);

            var fe = _cfg["Email:FrontendBaseUrl"];
            var link = $"{fe}/auth/reset?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";
            await _email.SendEmailAsync(user.Email, "Reset your password",
                $"Click <a href=\"{link}\">here</a> to reset your password. Link valid 2h.");
            return true;
        }
    }
}