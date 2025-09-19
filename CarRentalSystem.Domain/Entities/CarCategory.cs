using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class CarCategory : AuditableEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal DailyRateModifier { get; set; } = 1.0M;

        // Navigation property for one-to-many relationship with Car
      //  public ICollection<Car> Cars { get; set; } = new List<Car>();


    }
}
