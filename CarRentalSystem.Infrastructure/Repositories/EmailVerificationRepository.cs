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
    internal sealed class EmailVerificationRepository
        : RepositoryBase<EmailVerification>, IEmailVerificationRepository
    {
        private readonly ApplicationDbContext _ctx;
        public EmailVerificationRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

        public IQueryable<EmailVerification> Query() => _ctx.EmailVerifications.AsQueryable();

        public Task<EmailVerification?> FindByUserAndTokenAsync(Guid userId, string token, CancellationToken ct = default)
            => _ctx.EmailVerifications.FirstOrDefaultAsync(e => e.UserId == userId && e.Token == token, ct);

        public Task<bool> ExistsValidAsync(Guid userId, string token, CancellationToken ct = default)
            => _ctx.EmailVerifications.AnyAsync(e =>
                   e.UserId == userId && e.Token == token && !e.IsUsed && e.ExpiresAt > DateTime.UtcNow, ct);

        public Task SaveChangesAsync(CancellationToken ct = default)
            => _ctx.SaveChangesAsync(ct);
    }
}
