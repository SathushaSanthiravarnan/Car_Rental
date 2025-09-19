using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;

namespace CarRentalSystem.Application.Commands.Auth.Logout
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IRefreshTokenRepository _refreshTokens;

        public LogoutCommandHandler(IRefreshTokenRepository refreshTokens)
        {
            _refreshTokens = refreshTokens;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken ct)
        {
            var rt = await _refreshTokens.FindByTokenAsync(request.Dto.RefreshToken, ct);
            if (rt is null) return true;

            if (rt.RevokedAtUtc == null)
            {
                rt.RevokedAtUtc = DateTime.UtcNow;
                _refreshTokens.Update(rt);
                await _refreshTokens.SaveChangesAsync(ct);
            }
            return true;
        }
    }
}