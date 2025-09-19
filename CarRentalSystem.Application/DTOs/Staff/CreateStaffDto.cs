using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Staff
{
    public sealed record CreateStaffDto(
        Guid UserId,
        string? EmailForWork,
        string FirstName,
        string LastName,
        string? Phone,
        string? EmployeeCode,
        string? Notes,
        string StaffType,
        DateTime? DateOfBirth
    );
}
