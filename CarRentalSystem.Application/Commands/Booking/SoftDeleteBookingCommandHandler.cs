using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Booking
{
    public sealed class SoftDeleteBookingCommandHandler : IRequestHandler<SoftDeleteBookingCommand, bool>
    {
        private readonly IBookingRepository _bookings;
        private readonly IApplicationDbContext _db;

        public SoftDeleteBookingCommandHandler(IBookingRepository bookings, IApplicationDbContext db)
        {
            _bookings = bookings;
            _db = db;
        }

        public async Task<bool> Handle(SoftDeleteBookingCommand request, CancellationToken ct)
        {
            var booking = await _bookings.GetByIdAsync(request.BookingId, ct);
            if (booking is null)
            {
                return false;
            }

            booking.IsDeleted = true; // 'AuditableEntity' இலிருந்து வருகிறது
            await _db.SaveChangesAsync(ct);

            return true;
        }
    }
}
