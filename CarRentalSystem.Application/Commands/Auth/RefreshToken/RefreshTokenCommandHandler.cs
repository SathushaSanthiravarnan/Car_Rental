using AutoMapper;
using CarRentalSystem.Application.DTOs.Auth;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Auth.RefreshToken
{
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResultDto>
    {
        private readonly IRefreshTokenRepository _refreshTokens;
        private readonly IUserRepository _users;
        private readonly ITokenService _tokens;
        private readonly IConfiguration _cfg;
        private readonly IMapper _mapper;

        public RefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokens,
            IUserRepository users,
            ITokenService tokens,
            IConfiguration cfg,
            IMapper mapper)
        {
            _refreshTokens = refreshTokens; _users = users; _tokens = tokens; _cfg = cfg; _mapper = mapper;
        }

        public async Task<AuthResultDto> Handle(RefreshTokenCommand request, CancellationToken ct)
        {
            var rt = await _refreshTokens.FindByTokenAsync(request.Dto.RefreshToken, ct)
                     ?? throw new InvalidOperationException("Invalid refresh token.");

            if (rt.RevokedAtUtc != null || DateTime.UtcNow >= rt.ExpiresAtUtc)
                throw new InvalidOperationException("Refresh token expired or revoked.");

            var u = await _users.GetByIdAsync(rt.UserId, ct)
                    ?? throw new InvalidOperationException("User not found.");

            // rotate token
            var (access, exp) = _tokens.CreateAccessToken(u);
            var newRefresh = _tokens.CreateRefreshToken();

            rt.RevokedAtUtc = DateTime.UtcNow;
            rt.ReplacedByToken = newRefresh;
            _refreshTokens.Update(rt);

            await _refreshTokens.AddAsync(new Domain.Entities.RefreshToken
            {
                UserId = u.Id,
                Token = newRefresh,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(int.Parse(_cfg["Jwt:RefreshTokenDays"]!))
            }, ct);

            await _refreshTokens.SaveChangesAsync(ct);

            var authUser = _mapper.Map<AuthUserDto>(u);
            return new AuthResultDto(access, exp, newRefresh, authUser);
        }
    }
}
