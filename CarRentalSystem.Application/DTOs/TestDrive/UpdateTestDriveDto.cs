using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.TestDrive
{
    public sealed record UpdateTestDriveDto
    {
        public Guid Id { get; init; }

        [Required]
        public TestDriveStatus Status { get; init; }

        public string? Remarks { get; init; }

        public Guid? StaffId { get; init; } 
    }
}
