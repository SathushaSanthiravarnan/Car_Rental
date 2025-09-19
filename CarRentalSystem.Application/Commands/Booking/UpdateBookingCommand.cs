using CarRentalSystem.Application.DTOs.Booking;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Commands.Booking
{
    public sealed record UpdateBookingCommand(Guid BookingId, UpdateBookingDto Dto) : IRequest<BookingDto>;
}
