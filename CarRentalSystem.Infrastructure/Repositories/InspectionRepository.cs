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
    internal sealed class InspectionRepository : RepositoryBase<Inspection>, IInspectionRepository
    {
        private readonly ApplicationDbContext _ctx;

        public InspectionRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Inspection> Query() => _ctx.Set<Inspection>().AsQueryable();

        public async Task<Inspection?> FindByBookingIdAsync(Guid bookingId, CancellationToken ct = default)
        {
            return await _ctx.Inspections
                .FirstOrDefaultAsync(i => i.BookingId == bookingId, ct);
        }

        public async Task<bool> ExistsByBookingIdAsync(Guid bookingId, CancellationToken ct = default)
        {
            return await _ctx.Inspections
                .AnyAsync(i => i.BookingId == bookingId, ct);
        }
    }
}
