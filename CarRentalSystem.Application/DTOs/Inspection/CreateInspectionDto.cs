using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Inspection
{
    public sealed record CreateInspectionDto(
         Guid CarId,
         Guid? BookingId,
         Guid StaffId,
         InspectionType InspectionType,
         string? Notes,
         DateTime Date
     );
}
