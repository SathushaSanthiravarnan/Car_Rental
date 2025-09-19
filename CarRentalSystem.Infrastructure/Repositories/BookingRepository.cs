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
    internal sealed class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _ctx;

        public BookingRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Booking> Query() => _ctx.Set<Booking>().AsQueryable();

        public async Task<Booking?> FindByCarIdAsync(Guid carId, CancellationToken ct = default)
        {
            return await _ctx.Bookings.FirstOrDefaultAsync(b => b.CarId == carId, ct);
        }

        public async Task<List<Booking>> FindByCustomerIdAsync(Guid customerId, CancellationToken ct = default)
        {
            return await _ctx.Bookings.Where(b => b.CustomerId == customerId).ToListAsync(ct);
        }
    }
}
