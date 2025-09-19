using AutoMapper;
using CarRentalSystem.Application.DTOs.Booking;
using CarRentalSystem.Application.Interfaces.Persistence;
using CarRentalSystem.Application.Interfaces.Repositories;
using CarRentalSystem.Domain.Enums;
using CarRentalSystem.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Booking
{
    public sealed class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly IBookingRepository _bookings;
        private readonly ICarRepository _cars;
        private readonly IApplicationDbContext _db;
        private readonly IMapper _mapper;

        // Constructor
        public CreateBookingCommandHandler(IBookingRepository bookings, ICarRepository cars, IApplicationDbContext db, IMapper mapper)
        {
            _bookings = bookings;
            _cars = cars;
            _db = db;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken ct)
        {
            // ... (validation logic remains same)
            var car = await _cars.GetByIdAsync(request.Dto.CarId, ct)
                ?? throw new KeyNotFoundException("Car not found.");

            if (car.IsBooked)
            {
                throw new InvalidOperationException("Car is not available for booking.");
            }

            var duration = (request.Dto.ReturnDate - request.Dto.PickUpDate).TotalDays;
            var totalPrice = car.DailyRate * (decimal)duration;

            // Create new Address entities from DTOs
           
            // Map Booking DTO to Entity
            var booking = _mapper.Map<CarRentalSystem.Domain.Entities.Booking>(request.Dto);

            // Assign the new Address entities to the booking
         
            booking.TotalPrice = totalPrice;
            booking.Status = BookingStatus.Pending;

            await _bookings.AddAsync(booking, ct);
            car.IsBooked = true;
            await _db.SaveChangesAsync(ct);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
