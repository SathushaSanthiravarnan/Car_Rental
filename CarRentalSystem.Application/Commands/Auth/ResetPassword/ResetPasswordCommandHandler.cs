using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.ResetPassword
{
    public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IUserRepository _users;
        private readonly IEmailVerificationRepository _verifications;
        private readonly IRefreshTokenRepository _refreshTokens;
        private readonly IPasswordHasher _hasher;

        public ResetPasswordCommandHandler(
            IUserRepository users,
            IEmailVerificationRepository verifications,
            IRefreshTokenRepository refreshTokens,
            IPasswordHasher hasher)
        {
            _users = users; _verifications = verifications; _refreshTokens = refreshTokens; _hasher = hasher;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken ct)
        {
            var user = await _users.FindByEmailAsync(request.Dto.Email, ct)
                       ?? throw new InvalidOperationException("Invalid email.");

            var v = await _verifications.FindByUserAndTokenAsync(user.Id, request.Dto.Token, ct)
                    ?? throw new InvalidOperationException("Invalid token.");

            if (v.IsUsed || v.ExpiresAt < DateTime.UtcNow)
                throw new InvalidOperationException("Token expired or already used.");

            var (hash, salt) = _hasher.Hash(request.Dto.NewPassword);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            v.IsUsed = true;
            _verifications.Update(v);

            // revoke all active refresh tokens
            foreach (var rt in _refreshTokens.Query().Where(r => r.UserId == user.Id && r.RevokedAtUtc == null))
            {
                rt.RevokedAtUtc = DateTime.UtcNow;
                _refreshTokens.Update(rt);
            }

            await _verifications.SaveChangesAsync(ct);
            await _refreshTokens.SaveChangesAsync(ct);
            return true;
        }
    }
}