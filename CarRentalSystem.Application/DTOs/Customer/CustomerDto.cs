using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Customer
{
    public sealed record CustomerDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? Phone { get; init; }
        public string? EmailForContact { get; init; }
        public string? AlternatePhone { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? NationalIdNumber { get; init; }
        public string? DrivingLicenseNumber { get; init; }
        public DateTime? DrivingLicenseExpiryUtc { get; init; }
        public int LoyaltyPoints { get; init; }
        public bool IsBlacklisted { get; init; }
        public string? BlacklistReason { get; init; }
    }
}
