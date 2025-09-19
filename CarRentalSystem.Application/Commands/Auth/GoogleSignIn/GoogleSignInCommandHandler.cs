using AutoMapper;
using CarRentalSystem.Application.DTOs.Auth;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Application.Interfaces.Security;
using CarRentalSystem.Application.Interfaces.Services;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefreshTokenEntity = CarRentalSystem.Domain.Entities.RefreshToken;

namespace CarRentalSystem.Application.Commands.Auth.GoogleSignIn
{
    public sealed class GoogleSignInCommandHandler : IRequestHandler<GoogleSignInCommand, AuthResultDto>
    {
        private readonly IConfiguration _cfg;
        private readonly IUserRepository _users;
        private readonly ICustomerRepository _customers;
        private readonly IRefreshTokenRepository _refreshTokens;
        private readonly ITokenService _tokens;
        private readonly IGoogleTokenValidator _google;
        private readonly IMapper _mapper;

        public GoogleSignInCommandHandler(
            IConfiguration cfg,
            IUserRepository users,
            ICustomerRepository customers,
            IRefreshTokenRepository refreshTokens,
            ITokenService tokens,
            IGoogleTokenValidator google,
            IMapper mapper)
        {
            _cfg = cfg; _users = users; _customers = customers; _refreshTokens = refreshTokens;
            _tokens = tokens; _google = google; _mapper = mapper;
        }

        public async Task<AuthResultDto> Handle(GoogleSignInCommand request, CancellationToken ct)
        {
            var payload = await _google.ValidateAsync(request.Dto.IdToken, _cfg["GoogleOAuth:ClientId"]!, ct);

            var u = await _users.FindByEmailAsync(payload.Email, ct);
            if (u is null)
            {
                u = new User
                {
                    FirstName = payload.GivenName ?? "Guest",
                    LastName = payload.FamilyName ?? "",
                    Email = payload.Email,
                    EmailConfirmed = true,
                    PasswordHash = string.Empty,
                    PasswordSalt = string.Empty,
                    Role = UserRole.Customer,
                    GoogleSubjectId = payload.Subject,
                    GoogleEmail = payload.Email
                };

                await _users.AddAsync(u, ct);
                await _customers.AddAsync(new Customer { UserId = u.Id }, ct);
            }

            var (access, exp) = _tokens.CreateAccessToken(u);
            var refresh = _tokens.CreateRefreshToken();

            await _refreshTokens.AddAsync(new RefreshTokenEntity
            {
                UserId = u.Id,
                Token = refresh,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(int.Parse(_cfg["Jwt:RefreshTokenDays"]!))
            }, ct);
            await _refreshTokens.SaveChangesAsync(ct);

            var authUser = _mapper.Map<AuthUserDto>(u);
            return new AuthResultDto(access, exp, refresh, authUser);
        }
    }
}
