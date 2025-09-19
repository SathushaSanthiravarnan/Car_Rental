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
    internal sealed class StaffRepository : RepositoryBase<Staff>, IStaffRepository
    {
        private readonly ApplicationDbContext _ctx;
        public StaffRepository(ApplicationDbContext ctx) : base(ctx) => _ctx = ctx;

        public IQueryable<Staff> Query() => _ctx.Staffs.AsQueryable();

        public Task<bool> ExistsForUserAsync(Guid userId, CancellationToken ct = default) =>
    _ctx.Staffs.AsNoTracking().AnyAsync(s => s.UserId == userId, ct);

    }
}
