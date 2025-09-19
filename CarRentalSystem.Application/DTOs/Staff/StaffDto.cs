using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Staff
{
    public sealed record StaffDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? Phone { get; init; }
        public string? EmailForWork { get; init; }
        public StaffType Type { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public DateTime JoinedOnUtc { get; init; }
        public bool IsActive { get; init; }
        public string? EmployeeCode { get; init; }
        public string? Notes { get; init; }
    }
}
