using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Entities;
using CarRentalSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrastructure.Repositories
{
    internal sealed class RefreshTokenRepository
         : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _ctx;
        public RefreshTokenRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

        public IQueryable<RefreshToken> Query() => _ctx.RefreshTokens.AsQueryable();

        public Task<RefreshToken?> FindByTokenAsync(string token, CancellationToken ct = default)
            => _ctx.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token, ct);

        public async Task<bool> IsActiveAsync(string token, CancellationToken ct = default)
        {
            var rt = await _ctx.RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Token == token, ct);
            return rt != null && rt.RevokedAtUtc == null && DateTime.UtcNow < rt.ExpiresAtUtc;
        }

        public Task SaveChangesAsync(CancellationToken ct = default)
            => _ctx.SaveChangesAsync(ct);
    }
}
