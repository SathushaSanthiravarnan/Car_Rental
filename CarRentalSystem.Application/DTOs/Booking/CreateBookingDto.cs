using CarRentalSystem.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Booking
{
    public sealed record CreateBookingDto(
    Guid CarId,
    Guid CustomerId,
    DateTime PickUpDate,
    DateTime ReturnDate
);
}
