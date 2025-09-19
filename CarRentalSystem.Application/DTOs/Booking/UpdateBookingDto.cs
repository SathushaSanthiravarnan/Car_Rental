using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Booking
{
    public sealed record UpdateBookingDto(
    DateTime? PickUpDate,
    DateTime? ReturnDate,
    string? Remarks,
    BookingStatus? Status
  
);
}
