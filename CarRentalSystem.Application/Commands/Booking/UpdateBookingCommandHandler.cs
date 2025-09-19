using AutoMapper;
using CarRentalSystem.Application.DTOs.Booking;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Booking
{
    public sealed class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, BookingDto>
    {
        private readonly IBookingRepository _bookings;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UpdateBookingCommandHandler(IBookingRepository bookings, IApplicationDbContext db, IMapper mapper)
        {
            _bookings = bookings;
            _db = db;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(UpdateBookingCommand request, CancellationToken ct)
        {
            var booking = await _bookings.GetByIdAsync(request.BookingId, ct)
                ?? throw new KeyNotFoundException($"Booking with ID {request.BookingId} not found.");

            // Apply updates from the DTO to the entity.
            if (request.Dto.PickUpDate.HasValue)
            {
                booking.PickUpDate = request.Dto.PickUpDate.Value;
            }

            if (request.Dto.ReturnDate.HasValue)
            {
                booking.ReturnDate = request.Dto.ReturnDate.Value;
            }

            if (request.Dto.Status.HasValue)
            {
                booking.Status = request.Dto.Status.Value;
            }

            // Use AutoMapper to update complex objects like Location
          

            booking.Remarks = request.Dto.Remarks;

            // Save the changes
            _bookings.Update(booking);
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
