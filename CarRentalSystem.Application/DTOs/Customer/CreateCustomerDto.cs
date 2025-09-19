using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Customer
{
    public sealed record CreateCustomerDto(
        Guid UserId,
        string? EmailForContact,
        string? AlternatePhone,
        DateTime? DateOfBirth,
        string? NationalIdNumber,
        string? DrivingLicenseNumber,
        DateTime? DrivingLicenseExpiryUtc
    );
}
