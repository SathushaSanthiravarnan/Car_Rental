using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Inspection
{
    public sealed record InspectionDto
    {
        public Guid Id { get; init; }
        public Guid CarId { get; init; }
        public Guid? BookingId { get; init; }
        public Guid StaffId { get; init; }
        public InspectionType InspectionType { get; init; }
        public string? Notes { get; init; }
        public DateTime Date { get; init; }
    }
}
