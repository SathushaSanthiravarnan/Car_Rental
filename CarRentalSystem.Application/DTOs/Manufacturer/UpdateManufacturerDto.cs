using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.Manufacturer
{
    public sealed record UpdateManufacturerDto(
    string Name,
    string? Country,
    int? FoundedYear
);
}
