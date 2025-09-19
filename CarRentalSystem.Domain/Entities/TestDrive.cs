using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class TestDrive : AuditableEntity
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public Guid CarId { get; set; }
        public Car Car { get; set; } = default!;

        public DateTime PreferredDate { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public int DurationMinutes { get; set; }
        public TestDriveStatus Status { get; set; }
        public string? Remarks { get; set; }

        // Foreign key to the Staff entity
        public Guid? ApprovedBy { get; set; }

        // Navigation property to the Staff entity
        public Staff? ApprovedByStaff { get; set; }
    }
}
