using CarRentalSystem.Application.DTOs.Booking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Queries.Booking
{
    public sealed record GetBookingByIdQuery(Guid Id) : IRequest<BookingDto?>;
}
