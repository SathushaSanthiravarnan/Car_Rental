using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class Car : AuditableEntity
    {
        public Guid Id { get; set; }

        // Relationships
        public Guid CarModelId { get; set; }
        public CarModel CarModel { get; set; } = default!;

        public Guid CarStatusId { get; set; }
        public CarStatus CarStatus { get; set; } = default!;

        // Properties
        public string PlateNumber { get; set; } = default!;
        public string Color { get; set; } = string.Empty;
        public int Mileage { get; set; }
        public decimal DailyRate { get; set; }
        public bool IsBooked { get; set; }

        // Navigation property for one-to-many relationship with Bookings
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
