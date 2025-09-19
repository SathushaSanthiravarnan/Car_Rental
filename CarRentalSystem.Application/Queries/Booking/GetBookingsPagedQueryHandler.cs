using AutoMapper;
using AutoMapper.QueryableExtensions;
using CarRentalSystem.Application.DTOs.Booking;
using CarRentalSystem.Application.DTOs.Common;
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
    public sealed class GetBookingsPagedQueryHandler
    : IRequestHandler<GetBookingsPagedQuery, PagedResultDto<BookingDto>>
    {
        private readonly IBookingRepository _bookings;
        private readonly IMapper _mapper;

        public GetBookingsPagedQueryHandler(IBookingRepository bookings, IMapper mapper)
            => (_bookings, _mapper) = (bookings, mapper);

        public async Task<PagedResultDto<BookingDto>> Handle(GetBookingsPagedQuery request, CancellationToken ct)
        {
            var query = _bookings.Query();

            var total = await query.CountAsync(ct);

            var items = await query
                .Include(b => b.Car)      // Include related Car data
                .Include(b => b.Customer) // Include related Customer data
                .OrderByDescending(b => b.PickUpDate)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PagedResultDto<BookingDto>(items, total, request.Page, request.PageSize);
        }
    }
}
