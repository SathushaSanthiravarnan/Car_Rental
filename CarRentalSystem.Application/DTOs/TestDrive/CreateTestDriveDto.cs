using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.TestDrive
{
    public sealed record CreateTestDriveDto
    {
        public Guid CustomerId { get; init; }
        public Guid CarId { get; init; }
        public DateTime PreferredDate { get; init; }
        public TimeSpan PreferredTime { get; init; }
        public int DurationMinutes { get; init; } = 30; // Default duration
        public string? Remarks { get; init; }
    }
}
