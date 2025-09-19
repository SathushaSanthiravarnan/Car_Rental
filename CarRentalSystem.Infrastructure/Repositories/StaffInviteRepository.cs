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
    internal sealed class StaffInviteRepository : RepositoryBase<StaffInvite>, IStaffInviteRepository
    {
        private readonly ApplicationDbContext _ctx;
        public StaffInviteRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

        public IQueryable<StaffInvite> Query() => _ctx.StaffInvites.AsQueryable();

        public Task<StaffInvite?> FindByTokenAsync(string token, CancellationToken ct = default)
            => _ctx.StaffInvites.FirstOrDefaultAsync(i => i.Token == token, ct);

        public Task<StaffInvite?> FindByEmailAsync(string email, CancellationToken ct = default)
            => _ctx.StaffInvites.FirstOrDefaultAsync(i => i.Email == email, ct);
    }
}
