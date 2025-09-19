using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.DTOs.CarModelDto
{
    public sealed record CarModelDto
    {
        public Guid Id { get; init; }
        public Guid ManufacturerId { get; init; }
        public string ManufacturerName { get; init; } = default!;
        public Guid CategoryId { get; init; }
        public string CategoryName { get; init; } = default!;
        public string Name { get; init; } = default!;
        public int? YearIntroduced { get; init; }
        public string BodyType { get; init; } = default!;
        public string FuelType { get; init; } = default!;
        public string Transmission { get; init; } = default!;
    }
}
