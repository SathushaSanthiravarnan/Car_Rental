using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class CarStatus : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = string.Empty;

        // This flag determines if a car with this status can be booked.
        public bool IsAvailableForRental { get; set; }

        // Navigation property to link back to the cars with this status (one-to-many relationship).
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
