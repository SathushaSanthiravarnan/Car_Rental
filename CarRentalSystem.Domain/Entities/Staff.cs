using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class Staff : AuditableEntity
    {
        // 1:1 User link
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        // Profile
        public string? EmailForWork { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public StaffType Type { get; set; } = StaffType.FrontDesk;
        public DateTime JoinedOnUtc { get; set; } = DateTime.UtcNow;

        // Employment
        public bool IsActive { get; set; } = true;
        public string? EmployeeCode { get; set; } // human-friendly code (e.g., "STF-0001")
        public string? Notes { get; set; }

    }
}
