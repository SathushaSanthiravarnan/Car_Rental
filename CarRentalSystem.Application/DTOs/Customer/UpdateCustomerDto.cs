using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Customer
{
    public sealed record UpdateCustomerDto(
        string? EmailForContact,
        string? AlternatePhone,
        DateTime? DateOfBirth,
        string? NationalIdNumber,
        string? DrivingLicenseNumber,
        DateTime? DrivingLicenseExpiryUtc,
        bool? IsBlacklisted,
        string? BlacklistReason
    );
}
