using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Booking;
using CarRentalSystem.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Booking
{
    public sealed class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto?>
    {
        private readonly IBookingRepository _bookings;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingRepository bookings, IMapper mapper)
        {
            _bookings = bookings;
            _mapper = mapper;
        }

        public async Task<BookingDto?> Handle(GetBookingByIdQuery request, CancellationToken ct)
        {
            return await _bookings.Query()
                .Where(b => b.Id == request.Id)
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
        }
    }
}
