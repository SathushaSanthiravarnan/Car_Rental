using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class Customer : AuditableEntity
    {
        // 1:1 User link
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        // Profile
        public string? AlternatePhone { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Identification (KYC style)
        public string? NationalIdNumber { get; set; }     // NIC/SSN/NRIC
        public string? DrivingLicenseNumber { get; set; } // Rental verification
        public DateTime? DrivingLicenseExpiryUtc { get; set; }

        // Contact/Address
        public string? EmailForContact { get; set; }

        // Loyalty / Risk
        public int LoyaltyPoints { get; set; } = 0;
        public bool IsBlacklisted { get; set; } = false;
        public string? BlacklistReason { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
