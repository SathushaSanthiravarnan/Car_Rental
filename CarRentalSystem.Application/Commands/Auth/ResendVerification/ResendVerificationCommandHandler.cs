using CarRentalSystem.Application.Interfaces.Notifications;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Auth.ResendVerification
{
    public sealed class ResendVerificationCommandHandler : IRequestHandler<ResendVerificationCommand, bool>
    {
        private readonly IUserRepository _users;
        private readonly IEmailVerificationRepository _verifications;
        private readonly IEmailSender _email;
        private readonly IConfiguration _cfg;

        public ResendVerificationCommandHandler(
            IUserRepository users,
            IEmailVerificationRepository verifications,
            IEmailSender email,
            IConfiguration cfg)
        {
            _users = users; _verifications = verifications; _email = email; _cfg = cfg;
        }

        public async Task<bool> Handle(ResendVerificationCommand request, CancellationToken ct)
        {
            var user = await _users.FindByEmailAsync(request.Dto.Email, ct)
                       ?? throw new InvalidOperationException("Email not found.");

            if (user.EmailConfirmed) return true;

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
            await _verifications.AddAsync(new Domain.Entities.EmailVerification
            {
                UserId = user.Id,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24),
                IsUsed = false
            }, ct);
            await _verifications.SaveChangesAsync(ct);

            var fe = _cfg["Email:FrontendBaseUrl"];
            var link = $"{fe}/auth/verify?email={Uri.EscapeDataString(user.Email)}&token={Uri.EscapeDataString(token)}";
            await _email.SendEmailAsync(user.Email, "Confirm your email",
                $"Click <a href=\"{link}\">here</a> to verify. Link valid 24h.");

            return true;
        }
    }
}
