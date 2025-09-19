using AutoMapper;
using CarRentalSystem.Application.DTOs.Auth;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;
using MediatR;
using Microsoft.Extensions.Configuration;
using RefreshTokenEntity = CarRentalSystem.Domain.Entities.RefreshToken;
using CarRentalSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Auth.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResultDto>
    {
        private readonly IUserRepository _users;
        private readonly IRefreshTokenRepository _refreshTokens;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokens;
        private readonly IConfiguration _cfg;
        private readonly IMapper _mapper;

        public LoginCommandHandler(
            IUserRepository users,
            IRefreshTokenRepository refreshTokens,
            IPasswordHasher hasher,
            ITokenService tokens,
            IConfiguration cfg,
            IMapper mapper)
        {
            _users = users; _refreshTokens = refreshTokens; _hasher = hasher;
            _tokens = tokens; _cfg = cfg; _mapper = mapper;
        }

        public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken ct)
        {
            var u = await _users.FindByEmailAsync(request.Dto.Email, ct)
                    ?? throw new InvalidOperationException("Invalid credentials.");

            if (!u.EmailConfirmed)
                throw new InvalidOperationException("Email not confirmed.");

            if (!_hasher.Verify(u.PasswordHash, u.PasswordSalt, request.Dto.Password))
                throw new InvalidOperationException("Invalid credentials.");

            var (access, exp) = _tokens.CreateAccessToken(u);
            var refresh = _tokens.CreateRefreshToken();

            await _refreshTokens.AddAsync(new RefreshTokenEntity
            {
                UserId = u.Id,
                Token = refresh,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(int.Parse(_cfg["Jwt:RefreshTokenDays"]!)),
                CreatedByIp = request.Ip,
                UserAgent = request.UserAgent
            }, ct);

            await _refreshTokens.SaveChangesAsync(ct);

            var authUser = _mapper.Map<AuthUserDto>(u);
            return new AuthResultDto(access, exp, refresh, authUser);
        }
    }
}
