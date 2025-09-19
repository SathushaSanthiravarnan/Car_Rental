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
    internal sealed class TestDriveRepository : RepositoryBase<TestDrive>, ITestDriveRepository
    {
        private readonly ApplicationDbContext _ctx;
        public TestDriveRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<TestDrive> Query() => _ctx.TestDrives.AsQueryable();

        public async Task<TestDrive?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default)
        {
            return await _ctx.TestDrives
                .Include(td => td.Customer)
                    .ThenInclude(c => c.User)
                .Include(td => td.Car)
                .Include(td => td.ApprovedByStaff)
                    .ThenInclude(s => s!.User)
                .FirstOrDefaultAsync(td => td.Id == id, ct);
        }
    }
}
