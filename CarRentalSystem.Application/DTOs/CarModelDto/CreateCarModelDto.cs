using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.CarModelDto
{
    public sealed record  class CreateCarModelDto
    (
       Guid ManufacturerId,
            Guid CategoryId,
            string Name,
            int? YearIntroduced,
            string? BodyType,
            string? FuelType,
            string? Transmission
    );
}
