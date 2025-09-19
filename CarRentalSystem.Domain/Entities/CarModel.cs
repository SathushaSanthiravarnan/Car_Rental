using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Domain.Entities
{
    public class CarModel : AuditableEntity
    {
        public Guid Id { get; set; }

      
        public Guid ManufacturerId { get; set; }
       public Manufacturer Manufacturer { get; set; } = default!;

       
        public Guid CategoryId { get; set; }
        public CarCategory CarCategory { get; set; } = default!;

        public string Name { get; set; } = default!;
        public int? YearIntroduced { get; set; }
        public string BodyType { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
       
    }
}
