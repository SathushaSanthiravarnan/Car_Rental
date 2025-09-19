using CarRentalSystem.Application.DTOs.Car;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.Customer;
using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Booking
{
    public sealed record BookingDto
    {
        public Guid Id { get; init; }
        public CarDto Car { get; init; } = default!;
        public CustomerDto Customer { get; init; } = default!;
        public DateTime PickUpDate { get; init; }
        public DateTime ReturnDate { get; init; }
        public BookingStatus Status { get; init; }
        public decimal TotalPrice { get; init; }
       
    }
}
