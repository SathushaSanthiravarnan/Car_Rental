using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.TestDrive
{
    public sealed record TestDriveDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string CustomerName { get; init; } = default!;
        public Guid CarId { get; init; }
        public string CarPlateNumber { get; init; } = default!;
        public DateTime PreferredDate { get; init; }
        public TimeSpan PreferredTime { get; init; }
        public TestDriveStatus Status { get; init; }
        public string? Remarks { get; init; }
        public string? ApprovedByStaffName { get; init; }
    }
}
