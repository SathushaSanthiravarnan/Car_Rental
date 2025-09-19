using CarRentalSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class Inspection : AuditableEntity
    {
        public Guid CarId { get; set; }
        public Car Car { get; set; } = default!;

        public Guid? BookingId { get; set; }
        public Booking? Booking { get; set; }

        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = default!;

        public InspectionType InspectionType { get; set; }

        public string? Notes { get; set; }

        public DateTime Date { get; set; }
    }
}
