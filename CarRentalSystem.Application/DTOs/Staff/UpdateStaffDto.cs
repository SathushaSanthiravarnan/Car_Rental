using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Staff
{
    public sealed record UpdateStaffDto(
        string? EmailForWork,
        string? Phone,
        string? EmployeeCode,
        string? Notes,
        string? StaffType,
        bool? IsActive,
        DateTime? DateOfBirth
    );
}
